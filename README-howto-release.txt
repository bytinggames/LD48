windows:
	execute release-win.bat
	find .exe in .\LD48\bin\Release\netcoreapp3.1\win-x64\publish

linux:
	execute release-linux.bat
	move the created folder to a linux os: .\LD48\bin\Release\netcoreapp3.1\linux-x64\publish
	grant execution rights for the executable
	make a shortcut in the upper directory
	zip the whole thing with tar.xz

mac:
	execute release-mac.bat
	send .\LD48\bin\Release\netcoreapp3.1\mac-x64\publish to bernie
	receive zip from bernie