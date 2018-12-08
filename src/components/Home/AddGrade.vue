<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;添加级别</div>
  <div class="panel-body">
       <el-form ref="form"  label-width="80px">
  <el-form-item label="级别名称">
    <el-input v-model="grad.GNum" placeholder="请输入级别名称，例如2015,2016" type="number"></el-input>
    {{grad.GNum}}
  </el-form-item>
  
  <el-form-item>
    <el-button type="primary" @click="addGrade()">立即添加</el-button>
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
             grad:{
               GNum:''
             },
       }
    },
    methods:{
        addGrade:function(){
            this.$axios.post('/apis/User/AddGrade', {grade:this.grad}).then(res=>{
              if(res.data=="1")
              {
                this.$message({
                 message: '恭喜你，级别添加成功了',
                 type: 'success'
                 });
              }
              else{
                this.$message.error('错了哦，级别添加失败！');
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
