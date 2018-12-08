<template>
<div class="log"> 
<div class="top_div">
 <h1 style="text-align:center;color:#fff;padding-top:100px;">学生请假管理系统</h1>
</div>
<div style="background: rgb(255, 255, 255); margin: -100px auto auto; border: 1px solid rgb(231, 231, 231); border-image: none; width: 400px; height: 200px; text-align: center;">
<div style="width: 165px; height: 96px; position: absolute;">
<div class="tou">
</div>
<div class="initial_left_hand" id="left_hand"></div>
<div class="initial_right_hand" id="right_hand"></div></div>
<P style="padding: 30px 0px 10px; position: relative;">
  <el-input  v-model="user.AdminName" placeholder="请输入账号" prefix-icon="el-icon-tickets" style="width:320px;" size="large"></el-input>
     
</P>
<P style="position: relative;">   
    <el-input type="password"  v-model="user.AdminPwd" placeholder="请输入账号" prefix-icon="el-icon-view" style="width:320px;" size="large"></el-input>      
  </P>

<div style="height: 50px; line-height: 50px; margin-top: 30px; border-top-color: rgb(231, 231, 231); border-top-width: 1px; border-top-style: solid;">
<P style="margin: 0px 35px 20px 45px;">
          <el-button type="primary" style="width:100px;height:30px;font-size:20px;"  v-on:click="login()">登录</el-button>
     </P>
</div>
</div>
		   <div style="text-align:center;"></div>
           <p style="text-align:center;padding-top:150px;">{{copy}}</p>
    </div>
</template>


<script>

export default {

 name:'Login',
 data(){
   return{
      copy:"Copyright 2018 版权所有 李瑞清",
      user:{
          AdminName:'',
          AdminPwd:''
      },
      userName:'',
      userRoleName:'',
      gnName:''
   }
 },
 methods:{
     login:function(){
          this.$axios.post('/apis/User/Login', { admin: this.user }).then(
                        res => {
                           if(res.data!="")
                           { 
							   this.gnName=res.data[0].gnName
							   sessionStorage.userName=res.data[0].adminName
							   sessionStorage.roleName=res.data[0].role
							   this.userName=sessionStorage.userName
							   this.userRoleName=sessionStorage.roleName
							console.log(this.userName);
							console.log(this.userRoleName)
							   this.$router.push({path:'/Index'})
                           }
                           else
                           { 
                             this.$message.error('账号或密码错误！');
                           }
                        }
                    ).catch()
     }
 },
}
</script>
<style  scoped >

body{
	background: #ebebeb;
	font-family: "Helvetica Neue","Hiragino Sans GB","Microsoft YaHei","\9ED1\4F53",Arial,sans-serif;
	color: #222;
	font-size: 12px;
}
*{padding: 0px;margin: 0px;}
.top_div{
	background: #008ead;
	width: 100%;
	height: 400px;
}
.ipt{
	border: 1px solid #d3d3d3;
	padding: 10px 10px;
	width: 290px;
	border-radius: 4px;
	padding-left: 35px;
	-webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
	box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
	-webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
	-o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
	transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s
}
.ipt:focus{
	border-color: #66afe9;
	outline: 0;
	-webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075),0 0 8px rgba(102,175,233,.6);
	box-shadow: inset 0 1px 1px rgba(0,0,0,.075),0 0 8px rgba(102,175,233,.6)
}
.u_logo{
	background: url("../assets/img/username.png") no-repeat;
	padding: 10px 10px;
	position: absolute;
	top: 43px;
	left:30px;

}
.p_logo{
	background: url("../assets/img/password.png") no-repeat;
	padding: 10px 10px;
	position: absolute;
	top: 12px;
	left: 30px;
}
a{
	text-decoration: none;
}
.tou{
	background: url("../assets/img/tou.png") no-repeat;
	width: 97px;
	height: 92px;
	position: absolute;
	top: -87px;
	left: 140px;
}
.left_hand{
	background: url("../assets/img/left_hand.png") no-repeat;
	width: 32px;
	height: 37px;
	position: absolute;
	top: -38px;
	left: 150px;
}
.right_hand{
	background: url("../assets/img/right_hand.png") no-repeat;
	width: 32px;
	height: 37px;
	position: absolute;
	top: -38px;
	right: -64px;
}
.initial_left_hand{
	background: url("../assets/img/hand.png") no-repeat;
	width: 30px;
	height: 20px;
	position: absolute;
	top: -12px;
	left: 100px;
}
.initial_right_hand{
	background: url("../assets/img/hand.png") no-repeat;
	width: 30px;
	height: 20px;
	position: absolute;
	top: -12px;
	right: -112px;
}
.left_handing{
	background: url("../assets/img/left-handing.png") no-repeat;
	width: 30px;
	height: 20px;
	position: absolute;
	top: -24px;
	left: 139px;
}
.right_handinging{
	background: url("../assets/img/right_handing.png") no-repeat;
	width: 30px;
	height: 20px;
	position: absolute;
	top: -21px;
	left: 210px;
}


</style>


