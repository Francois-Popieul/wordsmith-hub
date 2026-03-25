VERSION ?= 1.0
IMAGE = ghcr.io/francois-popieul/wordsmithhub-api

build:
	docker build -t $(IMAGE):$(VERSION) .

push:
	docker push $(IMAGE):$(VERSION)

release: build push
