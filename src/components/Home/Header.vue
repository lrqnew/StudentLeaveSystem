<template>

  <div class="hd">
      <span class="left">学生请假管理系统</span>
      <div class="right">
           <!-- <span><i class="el-icon-bell">&nbsp;<a href="#">提 醒</a></i><el-badge :value="1" :max="10" class="item"></el-badge></span>
           <span><i class="el-icon-message">&nbsp;<a href="#">消 息</a></i><el-badge :value="1" :max="10" class="item"></el-badge></span>
           <span><i class="el-icon-date">&nbsp;<a href="#">进 度</a></i><el-badge :value="1" :max="10" class="item"></el-badge></span> -->
           <i class="el-icon-star-off"></i><span style="color:#28A9E0">&nbsp;{{roleName}} : {{userName}}</span>
           <i class="el-icon-circle-close-outline">&nbsp;<router-link to="/" >退出</router-link></i>
      </div>
     
  </div> 
</template>
<script>
export default {
    name:'headerInfo',
    data(){
        return{
             userName:sessionStorage.userName,
             roleName:sessionStorage.roleName
        }
     
          
    },
    methods:{
        exit(){
             sessionStorage.userName=null;
        }
    }
 
    
}
</script>
<style scoped>
hd{
    widows: 100%;
    height: 30px;
}
.right{
    float: right;
}
span{
    padding-right: 20px;
  
}
.left{
    color:#28A9E0;
    float: left;
    font-size: 24px;
    font-weight: bold;
}
.el-icon-edit{
    margin-top: -20px;
    z-index: 222;
}
</style>


