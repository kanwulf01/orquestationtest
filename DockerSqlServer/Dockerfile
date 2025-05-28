FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

RUN apt-get update \
 && apt-get install -y curl apt-transport-https gnupg \
 && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
 && curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list \
      > /etc/apt/sources.list.d/mssql-tools.list \
 && apt-get update \
 && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev \
 && ln -s /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd \
 && ln -s /opt/mssql-tools/bin/bcp /usr/bin/bcp \
 && apt-get clean && rm -rf /var/lib/apt/lists/*

RUN dos2unix init-db.sh

USER mssql

WORKDIR /usr/src/app
COPY --chown=mssql:mssql init-db.sh init.sql ./

ENTRYPOINT [ "/usr/src/app/init-db.sh" ]
