 export default [{
         title: 'group1',
         items: [{
                 path: '/',
                 icon: 'dashboard',
                 title: '首页'
             },
             {
                path: '/login',
                 icon: 'dashboard',
                 title: '登陆页'
             }
         ]
     }, {
         title: 'group2',
         items: [{
            path: '/about',
             icon: 'dashboard',
             title: '关于页'
         }]
     },
     {
         title: 'group3',
         items: [{
             icon: 'widgets',
             title: '子页面',
             items: [{
                path: '/page1-1',
                     title: '关于页',
                 },
                 {
                    path: '/page1-2',
                     title: '关于页'
                 }
             ]
         }]
     }
 ]