<template>
<div class="container">
    <div class="panel panel-default">
  <div class="panel-heading"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;假条注销</div>
   
  <div class="panel-body">
      <div class="search">
       <el-input placeholder="请输入学号进行查找" v-model="stuNum">
     <i slot="prefix" class="el-input__icon el-icon-search" ></i>
     </el-input>
     
      </div>
      <span class="btnsearch">
           <el-button icon="el-icon-search" circle @click="SelectLeaveByStuNum()"></el-button>
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
    <el-table-column prop="BeginDate" label="请假日期" :formatter="formatDate">
    </el-table-column>
    <el-table-column label="状态">
       <template slot-scope="scope">
        <el-button
          size="mini"
          type="danger"
          @click="handleUpdata(scope.$index, scope.row)">点击注销</el-button>
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
        stuNum:''
      }
    },
    filters: {
                
               
            },
    mounted(){
         this.selectLeave(),
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

        handleUpdata(index, row) {
        var a=Object.assign({}, row)
        this.stuNum=a.StuNum
       this.$confirm('此操作将永久注销假条, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
           this.$axios.get('/apis/User/UpdataByStuNum',{params:{stuNum:this.stuNum}}).then(res=>{
            if(res.data=="1")
              {
                this.$message({
                 message: '恭喜你，假条已经注销',
                 type: 'success'
                 });
                  this.selectLeave(),
                  this.selectTotal()
              }
              else{
                this.$message.error('错了哦，假条注销失败！');
              }
        })
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消注销假条'
          });          
        })
        },
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
        },
        selectLeave(){
            this.$axios.get('/apis/User/SelectLeave',{ params: { rows: this.rows, pagesize: this.pagesize } }).then(res=>{
                this.tableData=res.data
                console.log(res.data)
            })
        },
        handleSizeChange(val) {
            this.rows=val
            this.selectLeave()
        console.log(`每页 ${val} 条`);
      },
      handleCurrentChange(val) {
          this.pagesize=val
          this.selectLeave()
        console.log(`当前页: ${val}`);
      },
      selectTotal(){
          this.$axios.get('/apis/User/Total').then(res=>{
              this.total=res.data
          })
      },
      SelectLeaveByStuNum(){
          this.$axios.get('/apis/User/SelectLeaveBy',{ params: { rows: this.rows, pagesize: this.pagesize,stuNum:this.stuNum } }).then(res=>{
                this.tableData=res.data
                if(res.data=="")
                {
                   this.$message.error('错了哦，此学生没有请假哦！');
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
