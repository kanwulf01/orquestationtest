#!/bin/bash

# Resolver hostname interno
echo "127.0.0.1 kafka-server" >> /etc/hosts

# Generar y formatear almacenamiento KRaft
CLUSTER_ID=$(${KAFKA_HOME}/bin/kafka-storage.sh random-uuid)
${KAFKA_HOME}/bin/kafka-storage.sh format -t $CLUSTER_ID -c ${KAFKA_HOME}/config/kraft/server.properties

# Iniciar Kafka en background
exec ${KAFKA_HOME}/bin/kafka-server-start.sh ${KAFKA_HOME}/config/kraft/server.properties &

# Esperar hasta que Kafka acepte conexiones
while ! ${KAFKA_HOME}/bin/kafka-topics.sh --list --bootstrap-server kafka:9001 > /dev/null 2>&1; do
  echo "Esperando que Kafka esté listo..."
  sleep 1
done

# Crear topics definidos en KAFKA_TOPICS
IFS=',' read -ra TOPICS <<< "${KAFKA_TOPICS}"
for topic in "${TOPICS[@]}"; do
    ${KAFKA_HOME}/bin/kafka-topics.sh --create \
      --topic "$topic" \
      --partitions ${KAFKA_PARTITIONS} \
      --bootstrap-server kafka:9001
done

# Mantener el contenedor vivo
tail -f /dev/null