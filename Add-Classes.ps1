param (
    [string[]]$FileNames
)

foreach ($fileName in $FileNames) {
    # Check if the file exists
    if (-Not (Test-Path $fileName)) {
        Write-Error "File '$fileName' does not exist"
        continue
    }

    # Check if the file has a .cs extension
    if ([System.IO.Path]::GetExtension($fileName) -ne ".cs") {
        Write-Error "File '$fileName' is not a .cs file"
        continue
    }

    try {
        # Read the file content
        $classDefinition = Get-Content -Path $fileName -Raw

        # Use Add-Type to add the class definition
        Add-Type -TypeDefinition $classDefinition -Language CSharp
        Write-Host "Successfully added class definition from file: $fileName"
    } catch {
        Write-Error "Failed to add class definition from: $fileName"
        Write-Error $_.Exception.Message
    }
}
