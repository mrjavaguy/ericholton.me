Title: Building a Stand Up Bot for Slack - Prereqs
Published: 08/19/2019
Tags: 
  - Open-faas
  - Slack
  - Kubernetes
  - Helm
---
# Building a Stand up Bot for Slack

A while back for hack week at work, I started working on a Stand up bot for Slack. Due to a issues around the release of a new product at work, I did not get much time to work on the bot. I have decided to use my 10% time to work on the bot but I am going to approach it from a different way then what we had done for hack week.

## Functions everywhere

I will be building this with each part of the bot as a separtate function. These function will be hosted in [Open-faas](https://www.openfaas.com/).

### Prerequisites

- A Kubernetes Cluster
- Helm
- Open-faas cli

### Setting up Open-fass

To start with you need to have a Kubernetes cluster to host Open-faas. For local testing, I am using Docker for Windows with Kubernetes enabled. Deploying Open-fass is fairly easy.

 - First install `tiller` into Kubernetes.

 ```
 >kubectl -n kube-system create sa tiller
 >kubectl create clusterrolebinding tiller-cluster-rule --clusterrole=cluster-admin --serviceaccount=kube-system:tiller
helm init --skip-refresh --upgrade --service-account tiller
 ```

 - Then add namespaces for Open-faas

 ```
  kubectl apply -f https://raw.githubusercontent.com/openfaas/faas-netes/master/namespaces.yml
```

- Get the helm repro for Open-faas

```
helm repo add openfaas https://openfaas.github.io/faas-netes/
```

- Use helm to install Open-faas

```
helm upgrade openfaas --install openfaas/openfaas --namespace openfaas  --set functionNamespace=openfaas-fn --set operator.create=true
```

- Check if the deploy worked

```
kubectl --namespace=openfaas get deployments
```

should look like

```
NAME           DESIRED   CURRENT   UP-TO-DATE   AVAILABLE   AGE
alertmanager   1         1         1            1           1d
gateway        1         1         1            1           1d
nats           1         1         1            1           1d
prometheus     1         1         1            1           1d
queue-worker   1         1         1            1           1d
```

