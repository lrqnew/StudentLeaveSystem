<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;添加学生</div>
  <div class="panel-body">
       <el-form ref="form" :model="form" label-width="80px">
  <el-form-item  label="学号">
    <el-input v-model="form.stuNum" placeholder="请输入学号"></el-input>
  </el-form-item>
   <el-form-item label="姓名">
    <el-input v-model="form.stuName" placeholder="请输入姓名"></el-input>
  </el-form-item>
   <el-form-item label="手机号">
    <el-input v-model="form.phone" placeholder="请输入手机号"></el-input>
  </el-form-item>
  <el-form-item label="级别">
    <el-select v-model="form.Gid" placeholder="请选择级别" @focus="selectGrade()">
      <el-option v-for="item in grade" :key="item.Gid" :label="item.GNum" :value="item.Gid"></el-option>
    </el-select>
  </el-form-item>
  <el-form-item label="班级">
    <el-select v-model="form.Cid" placeholder="请选择班级" @focus="selectClass()">
      <el-option v-for="item in sClass" :key="item.Cid" :label="item.CName" :value="item.Cid"></el-option>
     
    </el-select>
  </el-form-item>
 
  <el-form-item>
    <el-button type="primary" @click="addStudent()">立即添加</el-button>
  
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
           form: {
          stuNum: '',
          stuName: '',
          phone: '',
          Gid:'',
          Cid:''
        },
        grade:{},
        sClass:{}
       }
    },
    methods:{
      selectGrade:function(){
        this.$axios.get("/apis/User/selectGrade").then(
          res=>{
            
            this.grade=res.data
        })
      },
      selectClass:function(){
      
        this.$axios.get("/apis/User/selectClass",{params:{gid:this.form.Gid}}).then(
          res=>{
            this.sClass=res.data
        })
      },
      addStudent:function(){
        this.$axios.post("/apis/User/AddStudent",{stu:this.form}).then(res=>{
          if(res.data=="1")
          {
            this.$message({
                 message: '恭喜你，学生添加成功了',
                 type: 'success'
                 });
          }
          else
          {
            this.$message.error('错了哦，学生添加失败！');
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
