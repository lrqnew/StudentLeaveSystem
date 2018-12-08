<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;添加用户</div>
  <div class="panel-body">
       <el-form ref="form"  label-width="80px">
          <el-form-item label="姓名">
    <el-input  placeholder="请输姓名" v-model="UserInfo.AdminName"></el-input>
  </el-form-item>
   <el-form-item label="职务">
   <el-select v-model="UserInfo.RoleId"  placeholder="请选择职务" @focus="selectRole()">
      <el-option v-for="item in roles" :key="item.RoleId" :label="item.RoleName" :value="item.RoleId" ></el-option>
    </el-select>
  </el-form-item>
   <el-form-item label="密码">
    <el-input  placeholder="请输入密码" v-model="UserInfo.AdminPwd"></el-input>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="addUser()">添加信息</el-button>
  
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
        UserInfo:{
         AdminName:'',
         AdminPwd:'',
         RoleId:''
        },
        roles:''
      }
    },
    methods:{
      //查询职务
      selectRole(){
          this.$axios.get('/apis/Admin/SelectRole').then(res=>{
               this.roles=res.data
          })
      },
      addUser(){
        this.$axios.post("/apis/Admin/AddUser",{admin:this.UserInfo}).then(res=>{
             if(res.data=="1")
             {
                this.$message({
                 message: '恭喜你，用户添加成功了',
                 type: 'success'
                 });
              }
              else{
                this.$message.error('错了哦，用户添加失败！');
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
