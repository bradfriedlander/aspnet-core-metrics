# demoWebApp
This is a simple MVC project that demonstrates how Magenic Metrics works for controllers that serve a user interface.

To use this project in a development environment, you will need to create the user secrets in your roaming folder.
```
...\AppData\Roaming\Microsoft\UserSecrets\ec4a4551-c405-4c90-ade2-9b6719cdc813\secrets.json
```

The contents will look like the following.

```json
{
  "MetricServiceSettings": {
    "MetricServiceConnection": "Data Source=.;Initial Catalog=DevOpsMetrics;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
  },
  "Https": {
    "File": "localhost.pfx",
    "Password": "XXX"
  }
}
```
