#!/bin/sh

# Imprimir mensaje de inicio
echo "Iniciando el script de actualización de precios..."

# Obtener el token de autenticación
echo "Obteniendo el token de autenticación..."
response=$(curl -s -X POST "http://api:8080/identity/login?useCookies=false&useSessionCookies=false" \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@domain.com", "password": "Admin1234!"}')

# Imprimir la respuesta del endpoint de autenticación
echo "Respuesta del endpoint de autenticación: $response"

token=$(echo $response | jq -r '.accessToken')

# Imprimir el token obtenido
echo "Token obtenido: $token"

# Llamar al endpoint de actualización de precios
echo "Llamando al endpoint de actualización de precios..."
update_response=$(curl -s -X POST "http://api:8080/api/ActualizarPrecios" \
  -H "Authorization: Bearer $token" \
  -H "Content-Type: application/json")

# Imprimir la respuesta del endpoint de actualización de precios
echo "Respuesta del endpoint de actualización de precios: $update_response"

# Imprimir mensaje de finalización
echo "Script de actualización de precios finalizado."