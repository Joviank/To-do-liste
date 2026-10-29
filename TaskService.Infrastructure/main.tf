terraform {
  required_providers {
    hcloud = {
      source  = "hetznercloud/hcloud"
      version = "~> 1.69"
    }
  }
}

provider "hcloud" {
  token = var.hcloud_token
}

module "app-server" {
    source = "./module/app-server"

    server_name = "ToScoobyDooo"
    ssh_key_name = var.ssh_key_name
}

output "server_ip" {
    value = module.app_server.server_ip
}