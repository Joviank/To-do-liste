const apiUrl = "/tasks";

// Henter token og legger det til i Authorization-headeren
function getAuthHeaders(extraHeaders = {}) {
  const token = localStorage.getItem("token");

  return {
    ...extraHeaders,
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
  };
}

async function loadTasks() {
  const response = await fetch(apiUrl, {
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    console.error("Kunne ikke hente tasks");
    return;
  }

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

  const response = await fetch(apiUrl, {
    method: "POST",
    headers: getAuthHeaders({
      "Content-Type": "application/json",
    }),
    body: JSON.stringify({
      Title: input.value,
    }),
  });

  if (!response.ok) {
    console.error("Kunne ikke legge til task");
    return;
  }

  input.value = "";
  await loadTasks();
}

async function toggleDone(id) {
  const response = await fetch(`/tasks/${id}/complete`, {
    method: "PATCH",
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    console.error("Kunne ikke oppdatere task");
    return;
  }

  await loadTasks();
}

document.getElementById("addTaskBtn").addEventListener("click", addTask);

loadTasks();
