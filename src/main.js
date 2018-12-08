// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import Router from 'vue-router'
import axios from 'axios'
//全局使用axios
Vue.prototype.$axios=axios
//引入bootstrap
import $ from 'jquery'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'
//引入 echarts
import echarts from 'echarts'
Vue.prototype.$echarts = echarts 
//引入elementui
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import Element from 'element-ui'
//引入组件
import Home from './components/Home/Index'
import Login from './components/Login'
//二级路由
import AddStudent from './components/Home/AddStudent'
import AddClass from './components/Home/AddClass'
import AddGrade from './components/Home/AddGrade'
import AddLeave from './components/Home/AddLeave'
import LeaveCancel from './components/Home/LeaveCancel'
import StudentInfo from './components/Home/StudentInfo'
import ClassInfo from './components/Home/ClassInfo'
import Default from './components/Home/Default'
import GradeInfo from './components/Home/GradeInfo'
import AddUser from './components/Home/AddUser'
import UpdateUser from './components/Home/UpdateUser'
import { networkInterfaces } from 'os';
Vue.use(Element, { size: 'small', zIndex: 3000 });
//2.使用router
Vue.use(Router)
//3.实例化router这个构造函数
let router=new Router({
  mode:'history',
  routes:[
    {
      path:'*',
      redirect:'/'
    },
  {
    //根目录
    path:'/',
    component:Login,

  },
  { 
        path:'/Index',
        component:Home,
        children:[
          {
            path:'/AddStudent',
            component:AddStudent
          },{
            path:'/AddClass',
            component:AddClass
          },{
            path:'/AddGrade',
            component:AddGrade
          },{
             path:'/AddLeave',
             component:AddLeave
          },{
            path:'/LeaveCancel',
            component:LeaveCancel
          },{
            path:'/StudentInfo',
            component:StudentInfo
          },{
            path:'/ClassInfo',
            component:ClassInfo
          },{
            path:'/Default',
            component:Default
          },{
            path:'/GradeInfo',
            component:GradeInfo
          },{
            path:'/AddUser',
            component:AddUser
          },{
            path:'/UpdateUser',
            component:UpdateUser
          }
        ]
  }
  
  
]
})
//全局守卫
router.beforeEach((to,from,nex)=>{
  if(to.path=='/'){
    nex()
  }
  else
  {
    if(sessionStorage.userName)
    {
      nex()
    }
    else
    {
      nex({path:'/'})
    }
  }

})

Vue.config.productionTip = false
//Vue.use(ElementUI)



/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
