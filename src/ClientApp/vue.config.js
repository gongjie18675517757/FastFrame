module.exports = {
    devServer: {
        // 设置主机地址
        host: 'localhost',
        // 设置默认端口
        port: 8081,
        // 设置代理
        proxy: {
            '/hub': {                 
                target: 'http://localhost:62431/',                
                ws: true,                
                changeOrigin: false
            },
			  '/api': {                 
                target: 'http://localhost:62431/',                
                ws: true,                
                changeOrigin: false
            }
        }
    }
}