# ## Dockerfile para un proyecto React con TypeScript y Vite

# # Etapa de construcción
# FROM node:18-alpine AS builder

# # Directorio de trabajo
# WORKDIR /app

# # Copiar archivos de dependencias e instalar
# COPY package.json package-lock.json ./
# RUN npm ci

# # Copiar el resto del código fuente
# COPY . .

# # Definir argumentos para variables VITE_
# ARG VITE_API_URL
# ENV VITE_API_URL=$VITE_API_URL

# # Construir la aplicación para producción
# RUN npm run build


# # Etapa de producción
# FROM nginx:stable-alpine

# # Eliminar configuración por defecto de nginx
# RUN rm /etc/nginx/conf.d/default.conf

# # Copiar nuestra configuración de nginx (opcional)
# # Puedes ajustar según tu setup; aquí un ejemplo básico
# COPY nginx.conf /etc/nginx/conf.d/app.conf

# # Copiar archivos estáticos desde el builder
# COPY --from=builder /app/dist /usr/share/nginx/html

# # Exponer el puerto
# EXPOSE 8082

# # Comando por defecto
# CMD ["nginx", "-g", "daemon off;"]


# ---- Builder ----
FROM node:18-alpine AS builder
WORKDIR /app

COPY package.json package-lock.json ./
RUN npm ci

COPY . .
RUN npm run build

# ---- Runtime ----
FROM node:18-alpine
WORKDIR /app

ARG VITE_API_URL
ENV VITE_API_URL=$VITE_API_URL
# Instala 'serve' globalmente
RUN npm install -g serve

# Copia los ficheros compilados
COPY --from=builder /app/dist ./dist

# Expone puerto (por defecto serve usa el 5000)
EXPOSE 5000

# Arranca el servidor estático
CMD ["serve", "-s", "dist", "--single", "-l", "5000"]

