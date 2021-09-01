module.exports = { 
  "transpileDependencies": [
    "vuetify"
  ],
  pages: {
    index: { 
      entry: 'src/main.js', 
      template: 'public/index.html', 
      filename: 'index.html', 
      title: 'xxx企业模块化开发模板', 
    }, 
    //subpage: 'src/subpage/main.js'
  },
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
      '/hangfire': {
        target: 'http://localhost:62431/',
        ws: true,
        changeOrigin: true
      },
    }
  }
}