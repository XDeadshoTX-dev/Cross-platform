Vagrant.configure("2") do |config|

  # Setting up the Ubuntu VM
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/bionic64"
    ubuntu.vm.hostname = "ubuntu-vm"
    ubuntu.vm.network "public_network"
    ubuntu.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 4
    end

    # Provisioning .NET Core 8.0 installation on Ubuntu
    ubuntu.vm.provision "shell", run: "always", inline: <<-SHELL
  wget https://dot.net/v1/dotnet-install.sh
  chmod +x dotnet-install.sh
  ./dotnet-install.sh --channel 8.0
  echo 'export PATH=$PATH:/home/vagrant/.dotnet' | sudo tee -a /etc/profile
  sudo snap install dotnet-sdk --classic
SHELL


  end

  # Setting up the Windows VM
config.vm.define "windows" do |windows|
  windows.vm.box = "gusztavvargadr/windows-10"
  windows.vm.hostname = "windows-vm"
  windows.vm.network "public_network"
  windows.vm.provider "virtualbox" do |vb|
    vb.memory = "4096"
    vb.cpus = 4
  end
  
  # Provisioning .NET Core 8 installation using Chocolatey
  windows.vm.provision "shell", run: "always", inline: <<-SHELL
    # Set execution policy to allow the installation script to run
    Set-ExecutionPolicy Bypass -Scope Process -Force

    # Install Chocolatey if it's not already installed
    if (-not (Get-Command "choco" -ErrorAction SilentlyContinue)) {
        Write-Host "Installing Chocolatey..."
        [System.Net.WebClient]::new().DownloadString('https://chocolatey.org/install.ps1') | Invoke-Expression
    }

    # Ensure that Chocolatey is installed
    if (Get-Command "choco" -ErrorAction SilentlyContinue) {
        Write-Host "Chocolatey successfully installed."

        # Install .NET Core 8 Runtime for ASP.NET and Desktop using Chocolatey
        choco install dotnet-runtime --version=8.0 -y
    } else {
        Write-Host "Failed to install Chocolatey. Exiting..."
        exit 1
    }

    # Verify that .NET Core 8 is successfully installed
    if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
        Write-Host ".NET Core 8 successfully installed."
        dotnet --list-sdks
        dotnet --list-runtimes
		exit 1
    } else {
        Write-Host ".NET Core 8 installation failed."
		Write-Host "Restarting Windows..."
        Restart-Computer -Force
    }
  SHELL
  
  windows.vm.provision "shell", run: "always", inline: <<-SHELL
    Write-Host "Checking if .NET Core 8 is installed after reboot..."

    # Recheck .NET Core installation
    if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
        Write-Host ".NET Core 8 is already installed."
        dotnet --list-sdks
        dotnet --list-runtimes
		exit 1
    } else {
        Write-Host ".NET Core 8 is not installed, retrying installation..."

        # Re-install .NET Core 8 Runtime for ASP.NET and Desktop using Chocolatey
        choco install dotnet-runtime --version=8.0 -y

        # Verify installation again
        if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
            Write-Host ".NET Core 8 successfully installed after retry."
            dotnet --list-sdks
            dotnet --list-runtimes
			exit 1
        } else {
            Write-Host ".NET Core 8 installation failed again. Manual intervention required."
            exit 1
        }
    }
  SHELL
end


  # Setting up the macOS VM using Jhcook's macOS Vagrant Box
  config.vm.define "macos" do |macos|
    macos.vm.box = "jhcook/macos-sierra"
    macos.vm.hostname = "macos-vm"
    macos.vm.network "public_network"
    macos.vm.provider "virtualbox" do |vb|
      vb.memory = "4096" # Configure memory allocation
      vb.cpus = 4        # Set the number of CPUs
    end

    # Provisioning .NET Core 8.0 installation on macOS
    macos.vm.provision "shell", inline: <<-SHELL
      /bin/bash -c "$(curl -fsSL https://dot.net/v1/dotnet-install.sh)"
      chmod +x dotnet-install.sh
      ./dotnet-install.sh --channel 8.0
      echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.zshrc
      source ~/.zshrc
    SHELL
  end
end
