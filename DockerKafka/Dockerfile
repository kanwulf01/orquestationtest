FROM eclipse-temurin:17-jdk

ENV KAFKA_VERSION=3.6.1
ENV SCALA_VERSION=2.13
ENV KAFKA_HOME=/opt/kafka
ENV KAFKA_PORT=9001
ENV KAFKA_TOPICS="permisos"
ENV KAFKA_PARTITIONS=3

# Instalar Kafka
RUN apt-get update && \
    apt-get install -y wget tar && \
    wget https://archive.apache.org/dist/kafka/${KAFKA_VERSION}/kafka_${SCALA_VERSION}-${KAFKA_VERSION}.tgz && \
    tar -xzf kafka_${SCALA_VERSION}-${KAFKA_VERSION}.tgz -C /opt && \
    mv /opt/kafka_${SCALA_VERSION}-${KAFKA_VERSION} ${KAFKA_HOME} && \
    rm kafka_${SCALA_VERSION}-${KAFKA_VERSION}.tgz

# Configurar KRaft, entrypoint y puertos
COPY server.properties ${KAFKA_HOME}/config/kraft/
COPY server.properties ${KAFKA_HOME}/config/
#COPY init-topics.sh /init-topics.sh
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
#RUN chmod +x /init-topics.sh

EXPOSE 9001 9093
VOLUME ["/tmp/kraft-combined-logs"]

ENTRYPOINT ["/entrypoint.sh"]