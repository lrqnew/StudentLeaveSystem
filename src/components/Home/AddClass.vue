<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;添加班级</div>
  <div class="panel-body">
       <el-form ref="form"  label-width="80px">
  <el-form-item label="班级名称">
    <el-input  placeholder="请输入班级名称" v-model="sclass.CName"></el-input>
  </el-form-item>
  <el-form-item label="级别">
    <el-select v-model="sclass.Gid"  placeholder="请选择级别" @focus="selectGrade()">
      <el-option v-for="item in grade" :key="item.Gid" :label="item.GNum" :value="item.Gid" ></el-option>
    </el-select>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="addClass()">立即添加</el-button>
  
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
          grade:[],
          sclass:{
              CName:'',
              Gid:'',
          }
      }
    },
    mounted(){
     
    },
    methods:{
      selectGrade:function(){
        this.$axios.get("/apis/User/selectGrade").then(
          res=>{
            this.grade=res.data
        })
      },
      addClass:function(){
        this.$axios.post("/apis/User/addClass",{clas:this.sclass}).then(res=>{
          if(res.data=="1")
          {
                this.$message({
                 message: '恭喜你，班级添加成功了',
                 type: 'success'
                 });
          }
          else{
             this.$message.error('错了哦，班级添加失败！');
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
