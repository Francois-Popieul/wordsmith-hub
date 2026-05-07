# Deployment Guide

## Production Setup (compose.prod.yaml)

### First-Time Setup

Before running the stack for the first time, generate SSL certificates:

```bash
docker-compose -f compose.prod.yaml run --rm certbot certonly --webroot -w /var/www/certbot -d wordsmith-hub.fr -d www.wordsmith-hub.fr
```

Then, start the stack:

```bash
docker-compose -f compose.prod.yaml up -d
```

### Database Access

Note: Databases are NOT exposed to localhost in production for security reasons. They are only accessible internally via Docker network to  prevent unauthorized external access to production databases.

### Certbot Renewal

The certbot service automatically renews certificates every 720 hours.
