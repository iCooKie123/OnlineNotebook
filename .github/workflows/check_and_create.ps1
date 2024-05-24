
param (
    [string]$specPath
)

if (-Not (Test-Path $specPath)) {
    New-Item -ItemType Directory -Path (Split-Path $specPath) -Force
    New-Item -ItemType File -Path $specPath -Force
}
