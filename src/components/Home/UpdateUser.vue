<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;修改密码</div>
  <div class="panel-body">
       <el-form ref="form"  label-width="80px">
           <el-form-item label="管理姓名" >
    <el-input   v-model="admin" :disabled="true"></el-input>
  </el-form-item>
   <el-form-item label="密码">
    <el-input  placeholder="请输入新密码" v-model="AdminPwd"></el-input>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="addUser()">立即修改</el-button>
  
  </el-form-item>
</el-form>
        
  </div>
</div>
</div>
</template>
<script>
export default {
    data(){
      return{
        // userName:sessionStorage.userName,
       AdminPwd:'',
       admin:sessionStorage.userName
      }
    },
    methods:{
      
      addUser(){
        this.$axios.get("/apis/Admin/UpdatePwd",{params:{newPwd:this.AdminPwd,admin:this.admin}}).then(res=>{
             if(res.data=="1")
             {
                this.$message({
                 message: '恭喜你，密码修改成功了,下次登录请使用新密码！',
                 type: 'success'
                 });
              }
              else{
                this.$message.error('错了哦，密码修改失败！');
              }
        })
      }
    }
}
</script>
<style scoped>
  .container{
      margin-top: 10px;
  }
  
</style>
