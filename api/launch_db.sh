#!/bin/bash

docker run -d --name AltenProject -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=+-@william-dechamps@-+ -e POSTGRES_DB=AltenProject -v $(pwd)/db_data:/var/lib/postgresql/data -p 5433:5432 postgres:alpine