INSTRUCCIONES DE EJECUCIÓN:


1. Para ejecutar la orquestación deberá ponerse en la rama Master
2. En la raíz del proyecto a la misma "altura" del archivo orquesation_permisos.yaml deberá ejecutar los siguientes 2 comandos

dos2unix DockerKafka/entrypoint.sh

dos2unix DockerSqlServer/init-db.sh

Estos 2 comandos son necesarios para que los respectivos dockerfile de sqlserver y de kafka puedan ejecutar archivos .sh dentro del contenedor

Después de ejecutar los 2 comandos mencionados anteriormente, ya se puede pasar a ejecutar la orquestación completa sin problemas

docker-compose -f .\orquesation_permisos.yaml build

docker-compose -f .\orquesation_permisos.yaml up -d 

