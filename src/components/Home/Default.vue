<template>
<div class="container">
    
    <div  class="lev">
<el-card :body-style="{ padding: '0px',background:'#409EFF' ,width:'250px'}">
      <img src="" class="image">
      <div style="padding: 14px;">
        <span>{{tadytal}} 人</span>
        <div class="bottom clearfix">
          <time class="time">今日请假人数</time>
          <el-button type="text" class="button" @click="leaveStory()">查看详情</el-button>
        </div>
      </div>
    </el-card>
    </div>
      <div class="lev">
<el-card :body-style="{ padding: '0px' ,background:'#E6A23C',width:'250px'}">
      <img src="" class="image">
      <div style="padding: 14px;">
        <span>{{yestal}} 人</span>
        <div class="bottom clearfix">
          <time class="time">昨日请假人数</time>
          <el-button type="text" class="button" @click="leaveYesDay()">查看详情</el-button>
        </div>
      </div>
    </el-card>
      </div>
     <div  class="lev">
<el-card :body-style="{ padding: '0px',background:'#F56C6C',width:'250px' }">
      <img src="" class="image">
      <div style="padding: 14px;">
        <span>{{weektal}} 人</span>
        <div class="bottom clearfix">
          <time class="time">本周请假人数</time>
          <el-button type="text" class="button" @click="leaveWeek()">查看详情</el-button>
        </div>
      </div>
    </el-card>
    </div>
     <div  class="lev">
<el-card :body-style="{ padding: '0px',background:'#909399',width:'250px' }">
      <img src="" class="image">
      <div style="padding: 14px;">
        <span>{{monthtal}} 人</span>
        <div class="bottom clearfix">
          <time class="time">本月请假人数</time>
          <el-button type="text" class="button" @click="leaveMonth()">查看详情</el-button>
        </div>
      </div>
    </el-card>
  
</div>
<div class="panel panel-default">
  <div class="panel-body">
      <el-card class="box-card qjrs" id="myChart">
  
     </el-card>
     <el-card class="box-card qjzl" >
      <div class="text item">
        <span class="info"> <i class="el-icon-info"></i>&nbsp;&nbsp;具体请假详情 (默认显示今天数据)</span>
        <div class="btnr">
          <el-button size="mini" type="primary" round @click="leaveStory1()">今天</el-button>
          <el-button size="mini" type="warning" round @click="leaveYesDay1()">昨天</el-button>
          <el-button size="mini" type="danger" round @click="leaveWeek1()">本周</el-button>
        </div>
      </div>
      <p style="text-align:center;margin-top:10px;">
        <span class="ba">请假</span>&nbsp;&nbsp;
          <span class="ba">销假</span>
      </p>
        <p style="text-align:center;margin-top:10px;">
        <span class="ba" style="padding-right:12px; color:#F56C6C;font-size:20px;">{{qjTotal}}</span>&nbsp;&nbsp;
          <span class="ba" style="color:#67C23A;font-size:20px;">{{xjTotal}}</span>
      </p>  
      <el-table :data="gridData1">
    <el-table-column property="StuName" label="姓名"></el-table-column>
    <el-table-column property="BeginDate" label="请假日期" :formatter="formatDate" ></el-table-column>
    <el-table-column property="EndDate" label="销假日期" :formatter="formatDate"></el-table-column>
    <el-table-column property="Statu" label="状态"></el-table-column>
  </el-table>
  <!--分页-->
    <div class="block" style="margin-top:15px;">
    <el-pagination
      @size-change="handleSizeChange1"
      @current-change="handleCurrentChange1"
      :current-page="currentPage4"
      :page-sizes="[5]"
      :page-size="rows"
      layout="total, sizes, prev, pager, next, jumper"
      :total="qjTotal">
    </el-pagination>
  </div>
     </el-card>
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
         tadytal:0,
         yestal:0,
         weektal:0,
         monthtal:0,
         xjTotal:0,
         qjTotal:0,
         currentPage1: 5,
        currentPage2: 5,
        currentPage3: 5,
        currentPage4: 4,
        rows:5,
        pagesize:1,
        total:0,
        gridData:[],
        gridData1:[],
        dialogTableVisible: false,
        dialogFormVisible: false,
        fun:'',
        grade:{},
        Gid:'',
        GNum:'',
        CName:'',
        fun1:''
    };
    },
 
    beforeUpdate(){
        this.drawLine()
    },
    created(){
        this.TodayTotal()
        this.YesterdayTotal()
        this.WeekTotal()
        this.MonthTotal()
        this.leaveStory1()
        // this.leaveWeek1()
        // this.leaveYesDay1()
    },
    methods:{
      drawLine(){
        // 基于准备好的dom，初始化echarts实例
        let myChart = this.$echarts.init(document.getElementById('myChart'))
        // 绘制图表
        myChart.setOption({
            title : {
        text: '请假人数统计',
        subtext: '',
        x:'center'
    },
    tooltip : {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
        orient: 'vertical',
        left: 'left',
        data: ['今日','昨日','本周','本月']
    },
    series : [
        {
            name: '请假人数',
            type: 'pie',
            radius : '55%',
            center: ['50%', '60%'],
            data:[
                {value:this.tadytal, name:'今日'},
                {value:this.yestal, name:'昨日'},
                {value:this.weektal, name:'本周'},
                {value:this.monthtal, name:'本月'},
            ],
            itemStyle: {
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }
    ]
        });
    },
        //日期格式化
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
        //今日请假人数
        TodayTotal(){
           this.$axios.get('/apis/Default/TodayTotal').then(res=>{
               if(res.data=="")
               {
                 this.total=0
               
               }
               else
               {
                this.total=res.data[0].total
            
               }
           
               this.tadytal=res.data[0].total
           })
        },
        TodayTotal1(){
          this.TodayXj()
           this.$axios.get('/apis/Default/TodayTotal').then(res=>{
               if(res.data=="")
               {
                 this.qjTotal=0
               
               }
               else
               {
                this.qjTotal=res.data[0].total
            
               }
           })
        },
        //今日销假人数
        TodayXj(){
          this.$axios.get('/apis/Default/TodayXj').then(res=>{
               if(res.data=="")
               {
                 this.xjTotal=0
               }
               else
               {
                this.xjTotal=res.data[0].xjTotal
               }
           })
        },
        //昨日请假人数
        YesterdayTotal(){
           this.$axios.get('/apis/Default/YesterdayTotal').then(res=>{
             if(res.data=="")
             {
               this.total=0
              
             }
             else
             {
              this.total=res.data[0].total
           
             }
          
             this.yestal=res.data[0].total
           })
        },
        YesterdayTotal1(){
          this.YesterdayXj()
           this.$axios.get('/apis/Default/YesterdayTotal').then(res=>{
             if(res.data=="")
             {
               this.qjTotal=0
              
             }
             else
             {
              this.qjTotal=res.data[0].total
           
             }
           })
        },
        //昨日销假人数
        YesterdayXj(){
              this.$axios.get('/apis/Default/YesXj').then(res=>{
               if(res.data=="")
               {
                 this.xjTotal=0
               }
               else
               {
                this.xjTotal=res.data[0].xjTotal
               }
           })
        },
        //本周请假人数
        WeekTotal(){
           this.$axios.get('/apis/Default/WeekTotal').then(res=>{
              if(res.data=="")
              {
               this.total=0
              
              }
              else{
                 this.total=res.data[0].total
               
              }
               
               this.weektal=res.data[0].total
                
           })
        },
         WeekTotal1(){
           this.WeekXj()
           this.$axios.get('/apis/Default/WeekTotal').then(res=>{
              if(res.data=="")
              {
               this.qjTotal=0
              }
              else{
                 this.qjTotal=res.data[0].total
               
              }
               
           })
        },
        // 本周销假人数
        WeekXj(){
this.$axios.get('/apis/Default/WeekXj').then(res=>{
               if(res.data=="")
               {
                 this.xjTotal=0
               }
               else
               {
                this.xjTotal=res.data[0].xjTotal
               }
           })
        },
        //本月请假人数
        MonthTotal(){
           this.$axios.get('/apis/Default/MonthTotal').then(res=>{
             if(res.data=="")
             {
               this.total=0
             }
             else
             {
              this.total=res.data[0].total 
             } 
             this.monthtal=res.data[0].total
              
           })
        },
         //今天请假记录
                leaveStory(){
                    this.dialogTableVisible = true
                    this.fun="T"
                    this.TodayTotal()
                    this.$axios.get('/apis/Default/Today',{params:{rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                leaveStory1(){
                    this.fun1="T"
                    this.TodayTotal1()
                    this.$axios.get('/apis/Default/Today',{params:{rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData1=res.data
                  })
                },
                //昨天请假记录
                leaveYesDay(){
                    this.dialogTableVisible = true
                    this.fun="Y"
                    this.YesterdayTotal()
                    this.$axios.get('/apis/Default/Yesterday',{params:{rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
               leaveYesDay1(){
                  this.fun1="y"
                    this.YesterdayTotal1()
                    this.$axios.get('/apis/Default/Yesterday',{params:{rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData1=res.data
                  })
                },
                //本周请假记录
                leaveWeek(){
                    this.dialogTableVisible = true
                    this.fun="W"
                    this.WeekTotal()
                    this.$axios.get('/apis/Default/Week',{params:{rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
                leaveWeek1(){
                    this.fun1="W"
                    this.WeekTotal1()
                    this.$axios.get('/apis/Default/Week',{params:{rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData1=res.data
                  })
                },
                //本月请假记录
                leaveMonth(){
                    this.dialogTableVisible = true
                    this.fun="M"
                    this.MonthTotal()
                    this.$axios.get('/apis/Default/Month',{params:{rows: this.rows, pagesize: this.pagesize}}).then(res=>{
                    this.gridData=res.data
                  })
                },
               
        handleSizeChange(val) {
            this.rows=val
            if(this.fun=="T")
            {
                this.leaveStory()
            }
            if(this.fun=="Y")
            {
                this.leaveYesDay()
            }
            if(this.fun=="W")
            {
                this.leaveWeek()
            }
             if(this.fun=="M")
            {
                this.leaveMonth()
            }
        
      },
      handleSizeChange1(val) {
            this.rows=val
            if(this.fun1=="T")
            {
                this.leaveStory1()
            }
            if(this.fun1=="Y")
            {
                this.leaveYesDay1()
            }
            if(this.fun1=="W")
            {
                this.leaveWeek1()
            }
             if(this.fun1=="M")
            {
                this.leaveMonth1()
            }
       
      },
      handleCurrentChange(val) {
          this.pagesize=val
         if(this.fun=="T")
            {
                this.leaveStory()
            }
            if(this.fun=="Y")
            {
                this.leaveYesDay()
            }
            if(this.fun=="W")
            {
                this.leaveWeek()
            }
             if(this.fun=="M")
            {
                this.leaveMonth()
            }
        
      },
      handleCurrentChange1(val) {
          this.pagesize=val
         if(this.fun1=="T")
            {
                this.leaveStory1()
            }
            if(this.fun1=="Y")
            {
                this.leaveYesDay1()
            }
            if(this.fun1=="W")
            {
                this.leaveWeek1()
            }
             if(this.fun1=="M")
            {
                this.leaveMonth1()
            }
        
      },
    }
}
</script>
<style scoped>
  .container{
      margin-top: 10px;
  }
   .time {
    font-size: 13px;
    color: #fff;
  }
  
  .bottom {
    margin-top: 13px;
    line-height: 12px;
  }

  .button {
    padding: 0;
    float: right;
    color: #fff;
  }

  .image {
    width: 100%;
    display: block;
  }

  .clearfix:before,
  .clearfix:after {
      display: table;
      content: "";
  }
  
  .clearfix:after {
      clear: both
  }
  .lev{
      float: left;
      margin-top: 10px;
      margin-left: 27px;
     
  }
  span{
    color: #fff;
  }
 .panel-default{
   height: 600px;
   background: #e9eef3;
   border: none;
 }
 .qjrs{
   width: 530px;
   height: 500px;
   margin-left: 10px;
   margin-top: 80px;
   padding-top: 30px;
 }
 .qjzl{
   width: 530px;
   height: 500px;
   margin-left:570px;
  margin-top:-500px;
 }
 .text{
   border-bottom: 2px solid #eee;
 }
 .info{
   color: #d98c96;
    display: block;
    margin-bottom: 15px;
 }
 .btnr{
   float: right;
   margin-top: -38px;
 }
 .ba{
  color: #b6b7bb;
  text-align: center;
 }
</style>
