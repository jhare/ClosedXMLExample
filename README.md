# ClosedXMLExample

Start in `http` mode with that dropdown that controls F5 behavior.

```
curl -X POST "@file=./yourfile.xlsx" "http://localhost:5161/api/excel"
```

And the CSVs end up in your `username/AppData/Local/Temp`
