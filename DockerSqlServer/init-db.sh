#!/bin/bash
set -e

/opt/mssql/bin/sqlservr &
pid=$!

echo "⏳ Esperando a que SQL Server arranque…"
sleep 30

echo "🚀 Ejecutando init.sql…"
/opt/mssql-tools/bin/sqlcmd \
  -S localhost -U SA -P "$SA_PASSWORD" \
  -i /usr/src/app/init.sql

echo "✅ Inicialización completada."

wait $pid