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
  
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab1 -t:Build
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab1 -t:Test
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab1 -t:Run
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab2 -t:Build
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab2 -t:Test
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab2 -t:Run
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab3 -t:Build
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab3 -t:Test
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab3 -t:Run
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
		
		choco install dotnet-8.0-sdk -y

        # Install .NET Core 8 Runtime for ASP.NET and Desktop using Chocolatey
        choco install dotnet-8.0-runtime -y
    } else {
        Write-Host "Failed to install Chocolatey. Exiting..."
    }

    # Verify that .NET Core 8 is successfully installed
    if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
        Write-Host ".NET Core 8 successfully installed."
        dotnet --list-sdks
        dotnet --list-runtimes
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
		
        # Run the dotnet build commands
        & {
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab1 -t:Build
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab1 -t:Test
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab1 -t:Run
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab2 -t:Build
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab2 -t:Test
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab2 -t:Run
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab3 -t:Build
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab3 -t:Test
            dotnet build C:/vagrant/Build.proj -p:Solution=Lab3 -t:Run
        }
    } else {
        Write-Host ".NET Core 8 is not installed, retrying installation..."

        # Install .NET Core 8 SDK and Runtime using Chocolatey
        choco install dotnet-8.0-sdk -y
        choco install dotnet-8.0-runtime -y

        # Verify installation again
        if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
            Write-Host ".NET Core 8 successfully installed after retry."
            dotnet --list-sdks
            dotnet --list-runtimes
			
            & {
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab1 -t:Build
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab1 -t:Test
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab1 -t:Run
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab2 -t:Build
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab2 -t:Test
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab2 -t:Run
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab3 -t:Build
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab3 -t:Test
                dotnet build C:/vagrant/Build.proj -p:Solution=Lab3 -t:Run
            }
        } else {
            Write-Host ".NET Core 8 installation failed again. Manual intervention required."
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

    # Provisioning installation of .NET Core SDK from the specified URL on macOS
    macos.vm.provision "shell", run: "always", inline: <<-SHELL
	  # Download VBoxDarwinAdditions from a third-party source
      curl -L -o VBoxGuestAdditions.iso "https://download.virtualbox.org/virtualbox/7.0.20/VBoxGuestAdditions_7.0.20.iso"

      # Mount the ISO file
      sudo hdiutil mount VBoxGuestAdditions.iso

      # Run the installer
      sudo installer -pkg /Volumes/VBox_GAs_7.0.20/VBoxDarwinAdditions.pkg -target /

      # Unmount the ISO
      sudo hdiutil unmount /Volumes/VBox_GAs_7.0.20

      # Clean up the ISO file
      # rm VBoxGuestAdditions.iso
	  
      # Download the .NET Core SDK package
      curl -o dotnet-sdk.pkg "https://download.visualstudio.microsoft.com/download/pr/cb2d65e1-ad90-4416-8e6a-3755f92ba39f/f498aca4950a038d6fc55cca75eca630/dotnet-sdk-2.2.207-osx-x64.pkg"

      # Install the package using sudo and the installer command
      sudo installer -pkg dotnet-sdk.pkg -target /

      # Clean up the downloaded package
      # rm dotnet-sdk.pkg

      # Set up environment variables for .NET Core
      echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.zshrc
      source ~/.zshrc
	  
	  curl -L -o git-installer.dmg https://sourceforge.net/projects/git-osx-installer/files/git-2.33.0-intel-universal-mavericks.dmg/download
	  
	  hdiutil attach git-installer.dmg -mountpoint /Volumes/Git
	  
	  sudo installer -pkg /Volumes/Git/git-2.33.0-intel-universal-mavericks.pkg -target /
	  
	  sudo git clone https://github.com/XDeadshoTX-dev/Cross-platform.git /Users/vagrant/Desktop/Cross-platform
	  
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab1 -t:Build
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab1 -t:Build
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab1 -t:Test
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab1 -t:Run
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab2 -t:Build
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab2 -t:Test
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab2 -t:Run
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab3 -t:Build
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab3 -t:Test
	  sudo dotnet build /Users/vagrant/Desktop/Cross-platform/Build.proj -p:Solution=Lab3 -t:Run
    SHELL
  end
end
