### Create DNS
```
aws route53 create-hosted-zone --name kube.simplicate.net --caller-reference 98
```
- Remember to setup the ns records in the parent domain (simplicate.net)
  - `kube.simplicate.net` `NS` to NS1, NS2, NS3, NS4 etc
- test DNS  
  - `dig NS kube.simplicate.net`

### Create Bucket
```
aws s3 mb s3://kube.simplicate.net
```

### Set State
```
export KOPS_STATE_STORE=s3://kube.simplicate.net
```

### Build Cluster
```
kops create cluster --zones=ap-southeast-1a kube.simplicate.net
```

### Examine
 - change editors:

  ``` 
  export KUBE_EDITOR="nano" 
  ```
  
 - list clusters, edit this cluster etc: 
 - be sure to check that the instance types for the nodes and master exist in the region.
 
  ```
  kops get cluster
  kops edit cluster kube.simplicate.net
  kops edit ig --name=kube.simplicate.net nodes
  kops edit ig --name=kube.simplicate.net master-ap-southeast-1a
  ```

### Run Cluster
Finally configure your cluster with: 
```
kops update cluster kube.simplicate.net
```

### Run Dashbaord
- Install Dashboard
```
kubectl create -f https://rawgit.com/kubernetes/dashboard/master/src/deploy/kubernetes-dashboard.yaml
```

- Run the proxy
```
kubectl proxy
```


### Delete Cluster and Remove bucket
- remove dns zone manually
```
kops delete cluster kube.simplicate.net
aws s3 rb s3://kube.simplicate.net
```
