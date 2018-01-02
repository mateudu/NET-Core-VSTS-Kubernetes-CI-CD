# .NET Core + Kubernetes CI/CD example using VSTS
Demo app, which demonstrates sample build and deployment by **VSTS** to **Kuberenetes** cluster at **Azure Container Services (ACS)**.
Entire procesure is decribed [**here**](https://cloudcooking.pl/index.php/2017/07/25/wdroz-mnie-tango-z-net-core-vsts-i-kubernetes-w-roli-glownej/) in Polish.

## Procedure overview
* **VSTS** builds **.NET Core** containers
* **VSTS** pushes containers to **Azure Container Registry**
* **VSTS** swaps **$ACR_DNS** and **$BUILD_ID** parameters in **deploy.yaml** with **Azure Container Registry** URL and build ID
* **VSTS** invokes **kubectl** with **deploy.yaml** file with swapped parameters.
