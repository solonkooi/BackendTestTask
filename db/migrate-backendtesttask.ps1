$scriptDir=$PSScriptRoot

$databaseName="backendtesttask"
$migrationDir="$scriptDir\backendtesttask"

$user="postgres"
$password="postgres"

docker run --rm -it  `
    --network=host  `
    -v ${migrationDir}:/migrations  `
    flyway/flyway migrate  `
    -locations=filesystem:/migrations  `
    -user="$user" -password="$password"  `
    -url="jdbc:postgresql://localhost/$databaseName"