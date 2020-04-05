module.exports = {
  "transpileDependencies": [
    "vuetify"
  ],
  devServer: {
    // 设置主机地址
    host: '0.0.0.0',
    // 设置默认端口
    port: 8081,
    // 设置代理
    proxy: {
      '/hub': {
        target: 'http://localhost:62431/',
        ws: true,
        changeOrigin: true
      },
      '/api': {
        target: 'http://localhost:62431/',
        ws: true,
        changeOrigin: true
      },
      '/swagger': {
        target: 'http://localhost:62431/',
        ws: true,
        changeOrigin: true
      },
    }
  }
}