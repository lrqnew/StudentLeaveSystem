<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;学生信息</div>
   
  <div class="panel-body">
      <div class="search">
       <el-input placeholder="请输入学号进行查找" v-model="stuNum">
     <i slot="prefix" class="el-input__icon el-icon-search" ></i>
     </el-input>
     
      </div>
      <span class="btnsearch">
           <el-button icon="el-icon-search" circle @click="SelectStuByStuNum()"></el-button>
      </span>

     <el-table
    :data="tableData"
    stripe
    style="width: 100%">
    <el-table-column
      prop="StuNum"
      label="学号"
      width="180">
    </el-table-column>
    <el-table-column
      prop="StuName"
      label="姓名"
      width="180">
    </el-table-column>
    <el-table-column
      prop="GNum"
      label="级别">
    </el-table-column>
    <el-table-column
      prop="CName"
      label="班级">
    </el-table-column>
    <el-table-column 
      prop="Phone"
      label="手机号">
    </el-table-column>

    <el-table-column label="操作">
       <template slot-scope="scope">
        <el-button
          size="mini"
          type="danger"
          @click="leaveStory(scope.$index, scope.row)" >请假记录</el-button>
      </template>
    </el-table-column>
  </el-table>
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
<!--dialog-->
<el-dialog title="请假记录" :visible.sync="dialogTableVisible">
  <el-table :data="gridData">
    <el-table-column property="Addresss" label="前往地点" width="150"></el-table-column>
    <el-table-column property="Reason" label="原因" width="200"></el-table-column>
    <el-table-column property="Principal" label="请假负责人"></el-table-column>
    <el-table-column property="BeginDate" label="开始日期" :formatter="formatDate" ></el-table-column>
    <el-table-column property="EndDate" label="结束日期" :formatter="formatDate"></el-table-column>
  </el-table>
</el-dialog>
  </div>
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
        gridData: [],
        dialogTableVisible: false,
        dialogFormVisible: false,
       
      }
    },
 
    mounted(){
         this.selectStudent(),
         this.selectTotal()
    },
    methods:{
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
                //请假记录
                leaveStory(index, row){
                  this.dialogTableVisible = true
                  var a=Object.assign({}, row)
                  this.stuNum=a.StuNum
                  console.log(this.stuNum);
                  this.$axios.get('/apis/User/SelectLeaveBy',{params:{rows: this.rows, pagesize: this.pagesize,stuNum:this.stuNum}}).then(res=>{
                    this.gridData=res.data
                  })
                },
        //查询学生数据
        selectStudent(){
            this.$axios.get('/apis/User/SelectStu',{ params: { rows: this.rows, pagesize: this.pagesize } }).then(res=>{
                this.tableData=res.data
                console.log(res.data)
            })
        },
        handleSizeChange(val) {
            this.rows=val
            this.selectStudent()
        console.log(`每页 ${val} 条`);
      },
      handleCurrentChange(val) {
          this.pagesize=val
          this.selectStudent()
        console.log(`当前页: ${val}`);
      },
      selectTotal(){
          this.$axios.get('/apis/User/StuTotal').then(res=>{
              this.total=res.data
          })
      },
      SelectStuByStuNum(){
          this.$axios.get('/apis/User/SelectStudent',{ params: {stuNum:this.stuNum } }).then(res=>{
                this.tableData=res.data
                if(res.data=="")
                {
                   this.$message.error('错了哦，数据库中没有此学生哦！');
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
  .block{
      float: right;
      margin-top:15px;
  }
  .search{
      width: 30%;
      margin-left:65%;
  }
  .btnsearch{
      float:right;
      margin-top: -32px;
      margin-right: 10px;
  }
</style>
