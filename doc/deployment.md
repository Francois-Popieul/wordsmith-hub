# Deployment Guide

## Production Setup (compose.prod.yaml)

### First-Time Setup

Before deploying on the server, create the required secrets inyour repository settings on Github:

- SERVER HOST
- SERVER USERNAME
- SERVER PRIVATE KEY
- VITE API BASE URL

Then upload the required files on your server:

- nginx.conf (removing the SSL blocks)
- .env.prod
- compose.prod.yaml

Deploy on your server pushing on the main branch, then run certbot with the correct compose file to generate a first certificate for your client and your API:

```bash
docker-compose -f compose.prod.yaml --env-file .env.prod run --rm certbot certonly --webroot -w /var/www/certbot -d your-domain.com -d www.your-domain.com
```

```bash
docker-compose -f compose.prod.yaml --env-file .env.prod run --rm certbot certonly --webroot -w /var/www/certbot -d your-domain-api.com -d www.your-domain-api.com
```

Upload the complete nginx.conf file including SSL blocks and restart nginx using:

```bash
docker compose -f compose.prod.yaml --env-file .env.prod restart nginx
```

Your site should be accessible from th client url.

### Database Access

Note: Databases are NOT exposed to localhost in production for security reasons. They are only accessible internally via Docker network to  prevent unauthorized external access to production databases.

### Certbot Renewal

The certbot service automatically renews certificates every 720 hours.

### 502 Bad Gateway Error

Nginx may not update the configuration after pushing new images on the server. In that case, force nginx to reload the configuration using:

```bash
docker-compose -f compose.prod.yaml --env-file .env.prod restart nginx
```
