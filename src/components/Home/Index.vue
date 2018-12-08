<template>
    <div>

        <el-container>
   <el-header>
      <headerInfo></headerInfo>
   </el-header>
    <el-container>
      <el-aside width="217px" >
        <left></left>
        
      </el-aside>

        <router-view></router-view>
  
   </el-container>
 </el-container>
    </div>
</template>
<script>
import Header from './Header'
import Left from  './Left'
import Left1 from  './Left1'

// import Left1 from './Left1'

export default {
    components:{
    headerInfo:Header,
    data(){
      return {
         gnName:'<'+this.$route.query.gnName+'>'+'<'+'/'+this.$route.query.gnName+'>',
         a:'5555'
      } 
    },
    left:Left,
    left1:Left1
  },

  mounted(){
     this.open3()
     this.$router.push({path:'/Default'})
  },
  methods: {
      open3() {
        this.$notify({
          title: '成功',
          message: '欢迎使用学生请假管理系统',
          type: 'success'
        });
      }
      }
}
</script>
<style scoped>
.el-header, .el-footer {
    background-color: #fff;
    color: #333;
    text-align: center;
    line-height: 60px;
    margin-top: 0px;
  }
  
  .el-aside {
    /* background-color: #D3DCE6; */
    color: #333;
    text-align: center;
    line-height: 200px;
    height:650px;

  }
  
  .el-main {
    
    color: #333;
    text-align: center;
    line-height: 160px;
    background: #fff;
    margin-top: 10px;
  }
   body > .el-container {
    margin-bottom: 40px;
  }
  
  .el-container:nth-child(5) .el-aside,
  .el-container:nth-child(6) .el-aside {
    line-height: 260px;
  }
  
  .el-container:nth-child(7) .el-aside {
    line-height: 320px;
  }
</style>


