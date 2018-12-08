<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;假条申请</div>
  <div class="panel-body">
       <el-form ref="form"  label-width="80px">
  <el-form-item label="学号">
    <el-input  placeholder="请输入学号" v-model="studentInfo.StuNum" @blur="selectStudent()"></el-input>
  
  </el-form-item>
   <el-form-item label="姓名">
    <el-input  placeholder="请输入姓名" v-model="studentInfo.StuName"></el-input>
  </el-form-item>
   <el-form-item label="手机号">
    <el-input  placeholder="请输入手机号" v-model="studentInfo.Phone"></el-input>
  </el-form-item>
  <el-form-item label="级别">
    <el-input  placeholder="请输入级别" v-model="studentInfo.GNum"></el-input>
  </el-form-item>
  <el-form-item label="班级">
    <el-input  placeholder="请输入班级" v-model="studentInfo.CName"></el-input>
  </el-form-item>
  <el-form-item label="开始时间">
      <el-date-picker type="date" placeholder="选择日期" v-model="studentInfo.BeginDate" style="width: 100%;"></el-date-picker>
  </el-form-item>
 <el-form-item label="结束时间">
      <el-date-picker type="date" placeholder="选择日期" v-model="studentInfo.EndDate" style="width: 100%;"></el-date-picker>
  </el-form-item>
<el-form-item label="前往地点">
    <el-input  placeholder="请输入地点" v-model="studentInfo.Addresss"></el-input>
  </el-form-item>
<el-form-item label="请假原因">
    <el-input type="textarea" placeholder="请输入请假原因" v-model="studentInfo.Reason" ></el-input>
  </el-form-item>
<el-form-item label="负责人">
    <el-input :disabled="true" v-model="studentInfo.Principal" ></el-input>
  </el-form-item>

  <el-form-item>
    <el-button type="primary" @click="addLeave()">假条申请</el-button>
  
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
       
        studentInfo:{
         StuNum:'',
         StuName:'',
         Phone:'',
         GNum:'',
         CName:'',
         BeginDate:'',
         EndDate:'',
         Addresss:'',
         Reason:'',
         Principal:sessionStorage.userName,
         Statu:0
        }
        
      }
    },
    methods:{
      selectStudent(){
        this.$axios.get('/apis/User/SelectStudent',{params:{stuNum:this.studentInfo.StuNum}}).then(res=>{
          this.studentInfo.StuName=res.data[0].StuName
          this.studentInfo.Phone=res.data[0].Phone
          this.studentInfo.GNum=res.data[0].GNum
          this.studentInfo.CName=res.data[0].CName
        })
      },
      addLeave(){
        this.$axios.post("/apis/User/AddLeave",{leave:this.studentInfo}).then(res=>{
             if(res.data=="1")
             {
                this.$message({
                 message: '恭喜你，假条申请添加成功了',
                 type: 'success'
                 });
              }
              else{
                this.$message.error('错了哦，假条申请失败！');
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
