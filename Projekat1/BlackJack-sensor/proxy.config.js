const PROXY_CONFIG = [
    {
        context: [
            "/hub/data",
        ],
        target: "http://192.168.1.200:6004",
        secure: false,
        changeOrigin: true
    }
  ]
  
  module.exports = PROXY_CONFIG;
  