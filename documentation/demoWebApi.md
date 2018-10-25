# demoWebApi
This is a simple MVC Web API projects that demonstrates how Magenic metrics works for API controllers.

To use this project in a development environment, you will need to create the user secrets in your roaming folder.
```
...\AppData\Roaming\Microsoft\UserSecrets\0aac46b8-ed28-4bb4-a3e1-96f155773c39\secrets.json
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
