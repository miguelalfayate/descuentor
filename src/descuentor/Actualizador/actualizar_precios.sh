#!/bin/sh

# Obtener el token de autenticación
response=$(curl -s -X POST "http://api:8080/identity/login?useCookies=false&useSessionCookies=false" \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@domain.com", "password": "Admin1234!"}')

token=$(echo $response | jq -r '.accessToken')

# Llamar al endpoint de actualización de precios
curl -s -X POST "http://api:8080/api/ActualizarPrecios" \
  -H "Authorization: Bearer $token" \
  -H "Content-Type: application/json"