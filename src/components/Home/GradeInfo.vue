<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;级别信息</div>
   
  <div class="panel-body">
     
      <div class="">

       <template>
  <el-select v-model="Gid"  placeholder="请选择级别">
    <el-option v-for="item in grade" :key="item.Gid" :label="item.GNum" :value="item.Gid"></el-option>
  </el-select>

  
</template>

      </div>
      <span class="btnsearch">
           <el-button icon="el-icon-search" circle @click="selectGradeByGid()"></el-button>
      </span>

     <el-table
    :data="tableData"
    stripe
    style="width: 100%">
    
    <el-table-column
      prop="GNum"
      label="级别"
      width="180">
      
    </el-table-column>
    <el-table-column
      prop="GNum"
      label="今日请假">
      <template slot-scope="scope">
        <el-button
          size="mini"
          type="primary"
          @click="leaveStory(scope.$index, scope.row)">请假详情</el-button>
      </template>
    </el-table-column>
    <el-table-column
      prop="CName"
      label="昨日请假">
      <template slot-scope="scope">
        <el-button
          size="mini"
          type="warning"
          @click="leaveYesDay(scope.$index, scope.row)">请假详情</el-button>
      </template>
    </el-table-column>
    <el-table-column 
      prop="Phone"
      label="本周请假">
      <template slot-scope="scope">
        <el-button
          size="mini"
          type="danger"
          @click="leaveWeek(scope.$index, scope.row)">请假详情</el-button>
      </template>
    </el-table-column>
    <el-table-column 
      prop="Phone"
      label="本月请假">
      <template slot-scope="scope">
        <el-button
          size="mini"
          type="info"
         @click="leaveMonth(scope.$index, scope.row)">请假详情</el-button>
      </template>
    </el-table-column>
  </el-table>
 
  </div>
  <!--dialog-->
<el-dialog title="请假记录" :visible.sync="dialogTableVisible"   width="80%" height="80%">
  <el-table :data="gridData">
    <el-table-column property="StuNum" label="学号" width="150"></el-table-column>
    <el-table-column property="StuName" label="姓名" width="200"></el-table-column>
    <el-table-column property="Phone" label="手机号"></el-table-column>
    <el-table-column property="CName" label="班级"></el-table-column>
    <el-table-column property="GNum" label="级别"></el-table-column>
    <el-table-column property="BeginDate" label="开始日期" :formatter="formatDate" ></el-table-column>
    <el-table-column property="EndDate" label="结束日期" :formatter="formatDate"></el-table-column>
    <el-table-column property="Addresss" label="前往地点"></el-table-column>
    <el-table-column property="Reason" label="原因"></el-table-column>
    <el-table-column property="Principal" label="请假负责人"></el-table-column>
  </el-table>
     <div slot="footer" class="dialog-footer">
        <div class="block">
    <el-pagination
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page="currentPage4"
      :page-sizes="[5,10,15,20]"
      :page-size="rows"
      layout="total, sizes, prev, pager, next, jumper"
      :total="total">
    </el-pagination>
  </div>
    </div>
  
</el-dialog>
</div>
</div>   
</template>
<script>
export default {
      data() {
      return {
        currentPage1: 5,
        currentPage2: 5,
        currentPage3: 5,
        currentPage4: 4,
        tableData: [],
        rows:5,
        pagesize:1,
        total:0,
        stuNum:'',
        Cid:'',
        sClass:{},
        grade:{},
        Gid:'',
        GNum:'',
        CName:'',
        gridData:[],
        dialogTableVisible: false,
        dialogFormVisible: false,
        fun:''
      }
    },
 
    mounted(){
         
    this.selectGrade()
    },
    methods:{
         selectGradeByGid(){
         this.$axios.get("/apis/User/selectGradeBy",{params:{gid:this.Gid}}).then(
          res=>{
            this.tableData=res.data
        })
      },
        formatDate(row, column, cellValue, index) {
         const daterc = row[column.property]
                   if(daterc!=null){
                       const dateMat= new Date(parseInt(daterc.replace("/Date(", "").replace(")/", ""), 10));
                      const year = dateMat.getFullYear();
                    const month = dateMat.getMonth() + 1;
                    const day = dateMat.getDate();
                    const hh = dateMat.getHours();
                    const mm = dateMat.getMinutes();
                    const ss = dateMat.getSeconds();
                    const timeFormat= year + "-" + month + "-" + day;
                    return timeFormat;
                   }
                },
                //今天请假记录
                leaveStory(index, row){
                  this.dialogTableVisible = true
                  var a=Object.assign({}, row)
                   this.fun="T"
                   this.GNum=a.GNum
                    this.selectTotal()
                  this.$axios.get('/apis/GradeInfo/TodayLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                 leaveStory1(){
                  
                    this.selectTotal()
                  this.$axios.get('/apis/GradeInfo/TodayLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                //昨天请假记录
                leaveYesDay(index, row){
                    this.dialogTableVisible = true
                  var a=Object.assign({}, row)
                  this.fun="Y"
                   this.GNum=a.GNum
                   
                    this.yesDayTotal()
                  this.$axios.get('/apis/GradeInfo/YesdayLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                leaveYesDay1(){
                   
                    this.yesDayTotal()
                  this.$axios.get('/apis/GradeInfo/YesdayLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                //本周请假记录
                leaveWeek(index, row){
                    this.dialogTableVisible = true
                  var a=Object.assign({}, row)
                   this.fun="W"
                   this.GNum=a.GNum
      
                    this.weekTotal()
                  this.$axios.get('/apis/GradeInfo/WeekLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                leaveWeek1(){
                    this.weekTotal()
                    this.$axios.get('/apis/GradeInfo/WeekLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data})
                },
                //本月请假记录
                leaveMonth(index, row){
                    this.dialogTableVisible = true
                  var a=Object.assign({}, row)
                  this.fun="M"
                  
                   this.GNum=a.GNum
                 
                   console.log(this.GNum)
                    this.monthTotal()
                  this.$axios.get('/apis/GradeInfo/MonthLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                leaveMonth1(){
                    this.monthTotal()
                  this.$axios.get('/apis/GradeInfo/MonthLeave',{params:{grade:this.GNum,rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
        
        //查询级别
        selectGrade:function(){
        this.$axios.get("/apis/GradeInfo/selectGrade").then(
          res=>{
             this.tableData=res.data
             this.grade=res.data
        })
      },
      SelectClassBy(){
          this.$axios.get('/apis/GradeInfo/SelectClassAndGradeBy',{ params: {gid:this.Gid,cid:this.Cid} }).then(res=>{
                this.tableData=res.data
                if(res.data=="")
                {
                   this.$message.error('错了哦，数据库中没有此班级哦！');
                }
            })
      },
      handleSizeChange(val) {
            this.rows=val
            if(this.fun=="T")
            {
                this.leaveStory1()
            }
            if(this.fun=="Y")
            {
                this.leaveYesDay1()
            }
            if(this.fun=="W")
            {
                this.leaveWeek1()
            }
             if(this.fun=="M")
            {
                this.leaveMonth1()
            }
        console.log(`每页 ${val} 条`);
      },
      handleCurrentChange(val) {
          this.pagesize=val

           if(this.fun=="T")
            {
                this.leaveStory1()
            }
            if(this.fun=="Y")
            {
                this.leaveYesDay1()
            }
            if(this.fun=="W")
            {
                this.leaveWeek1()
            }
             if(this.fun=="M")
            {
                this.leaveMonth1()
            }
        console.log(`当前页: ${val}`);
      },
      //今天请假数量
      selectTotal(){
          this.$axios.get('/apis/GradeInfo/TodayTotal',{params:{grade:this.GNum,cla:this.CName}}).then(res=>{
              this.total=res.data[0].total
          })
      },
      //昨天请假数量
      yesDayTotal(){
          this.$axios.get('/apis/GradeInfo/YesdayTotal',{params:{grade:this.GNum,cla:this.CName}}).then(res=>{
              this.total=res.data[0].total
          })
      },
      //本周请假数量
      weekTotal(){
          this.$axios.get('/apis/GradeInfo/WeekTotal',{params:{grade:this.GNum,cla:this.CName}}).then(res=>{
              this.total=res.data[0].total
          })
      },
      //本月请假数量
      monthTotal(){
          this.$axios.get('/apis/GradeInfo/MonthTotal',{params:{grade:this.GNum,cla:this.CName}}).then(res=>{
              this.total=res.data[0].total
          })
      }
    }
}
</script>
<style scoped>
  .container{
      margin-top: 10px;
  }
  .block{
      float: right;
      margin-top: -25px;
  }
  .search{
      width: 30%;
      margin-left:78%;
  }
  .btnsearch{
      float:right;
      margin-top: -30px;
      margin-right:850px;
  }
</style>
