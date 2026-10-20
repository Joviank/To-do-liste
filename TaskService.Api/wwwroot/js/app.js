const apiUrl = "http://api.localhost/tasks";

async function loadTasks() {
  const response = await fetch(apiUrl);
  const tasks = await response.json();

  const list = document.getElementById("taskList");
  list.innerHTML = "";

  for (const task of tasks) {
    const li = document.createElement("li");

    const span = document.createElement("span");
    span.style.textDecoration = task.isDone ? "line-through" : "none";
    span.textContent = task.title;

    const button = document.createElement("button");
    button.textContent = task.isDone ? "Undo" : "Done";

    button.addEventListener("click", () => toggleDone(task.id));

    li.appendChild(span);
    li.appendChild(button);
    list.appendChild(li);
  }
}

async function addTask() {
  const input = document.getElementById("taskInput");
  if (!input.value.trim()) return;

  await fetch(apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      Title: input.value,
    }),
  });
  input.value = "";
  loadTasks();
}

async function toggleDone(id) {
  await fetch(`/tasks/${id}/complete`, {
    method: "PATCH",
  });
  loadTasks();
}

document.getElementById("addTaskBtn").addEventListener("click", addTask);

loadTasks();
