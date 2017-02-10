
# Application

### Create services,pods and ingress 
```
kubectl create namespace star-dev
kubectl create namespace star-qa
kubectl create namespace star-prod
kubectl apply -f ./dev     --namespace star-dev
kubectl apply -f ./qa      --namespace star-qa
kubectl apply -f ./prod    --namespace star-prod
```

### View services,pods and ingress 
```
kubectl describe deployment 
kubectl describe service 
kubectl describe ing
```

### Delete services,pods and ingress 
```
kubectl delete deployments --all --namespace star-dev
kubectl delete pods        --all --namespace star-dev
kubectl delete services    --all --namespace star-dev
BEST NOT TO DELETE INGRESS BECAUSE IPS CHANGE
# kubectl delete ingress     --all --namespace star-dev 
```


### Delete Cluster
``` 
gcloud container clusters delete star-cluster
```