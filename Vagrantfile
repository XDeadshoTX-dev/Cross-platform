Vagrant.configure("2") do |config|

  # Setting up the Ubuntu VM
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/jammy64"
    ubuntu.vm.hostname = "ubuntu-vm"
    ubuntu.vm.network "public_network"
    ubuntu.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 4
    end

    # Provisioning .NET Core 8.0 installation on Ubuntu
    ubuntu.vm.provision "shell", run: "always", inline: <<-SHELL
	sudo snap install dotnet-runtime-80
	sudo snap alias dotnet-runtime-80.dotnet dotnet
	export DOTNET_ROOT=/snap/dotnet-runtime-80/current
	sudo apt-get update && sudo apt-get install -y dotnet-sdk-8.0
  
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab1 -t:Build
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab1 -t:Test
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab1 -t:Run
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab2 -t:Build
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab2 -t:Test
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab2 -t:Run
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab3 -t:Build
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab3 -t:Test
  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab3 -t:Run
  
  dotnet nuget add source http://192.168.50.62:5000/v3/index.json -n BaGet
  dotnet tool install --global DSvynarchuk --version 1.0.1
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
			
			dotnet nuget add source http://192.168.50.62:5000/v3/index.json -n BaGet
			dotnet tool install --global DSvynarchuk --version 1.0.1
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
				
				dotnet nuget add source http://192.168.50.62:5000/v3/index.json -n BaGet
				dotnet tool install --global DSvynarchuk --version 1.0.1
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
      # Download the .NET Core SDK package
      curl -o dotnet-sdk.pkg "https://download.visualstudio.microsoft.com/download/pr/cb2d65e1-ad90-4416-8e6a-3755f92ba39f/f498aca4950a038d6fc55cca75eca630/dotnet-sdk-2.2.207-osx-x64.pkg"

      # Install the package using sudo and the installer command
      sudo installer -pkg dotnet-sdk.pkg -target /

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
	  
	  mkdir -p ~/.nuget/NuGet
      cat <<EOF > ~/.nuget/NuGet/NuGet.Config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
    <add key="BaGet" value="http://192.168.50.62:5000/v3/index.json" />
  </packageSources>
</configuration>
EOF
      sudo dotnet tool install --global DSvynarchuk --version 1.0.1
    SHELL
  end
  
    config.vm.define "lab5" do |lab5|
    lab5.vm.box = "ubuntu/jammy64"
    lab5.vm.hostname = "lab5-vm"
    lab5.vm.network "public_network"
	lab5.vm.network "forwarded_port", guest: 5232, host: 5232
    lab5.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 4
    end

    # Provisioning .NET Core 8.0 installation on Ubuntu
    lab5.vm.provision "shell", run: "always", inline: <<-SHELL
	sudo snap install dotnet-runtime-80
	sudo snap alias dotnet-runtime-80.dotnet dotnet
	export DOTNET_ROOT=/snap/dotnet-runtime-80/current
	sudo apt-get update && sudo apt-get install -y dotnet-sdk-8.0
	
	sudo dotnet build /vagrant/Build.proj -p:Solution=Lab5 -t:Build
	sudo dotnet build /vagrant/Build.proj -p:Solution=Lab5 -t:Publish
	sudo dotnet build /vagrant/Build.proj -p:Solution=Lab5 -t:RunWeb
SHELL
  end
  config.vm.define "lab6" do |lab6|
    lab6.vm.box = "ubuntu/jammy64"
    lab6.vm.hostname = "lab6-vm"
    lab6.vm.network "public_network"
    lab6.vm.network "forwarded_port", guest: 5232, host: 5232
    lab6.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 4
    end

    # Provisioning .NET Core 8.0 installation on Ubuntu
    lab6.vm.provision "shell", run: "always", inline: <<-SHELL
      sudo snap install dotnet-runtime-80
      sudo snap alias dotnet-runtime-80.dotnet dotnet
      export DOTNET_ROOT=/snap/dotnet-runtime-80/current
      sudo apt-get update && sudo apt-get install -y dotnet-sdk-8.0
      sudo timedatectl set-timezone Asia/Bangkok

      # Install PostgreSQL
      sudo apt-get update
      sudo apt-get install -y wget gnupg2
      wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -
      echo "deb http://apt.postgresql.org/pub/repos/apt/ jammy-pgdg main" | sudo tee /etc/apt/sources.list.d/pgdg.list
      sudo apt-get update
      sudo apt-get install -y postgresql postgresql-contrib
	  
	  sudo dotnet tool install --global dotnet-ef
	  export PATH="$PATH:/root/.dotnet/tools"
	  
	  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab6 -t:Migrations
	  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab6 -t:Build
	  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab6 -t:Publish
	  sudo dotnet build /vagrant/Build.proj -p:Solution=Lab6 -t:RunWeb
    SHELL
  end
end
