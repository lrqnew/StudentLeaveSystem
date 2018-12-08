<template>
    <div>
   
  <el-radio-group v-model="isCollapse" style="margin-top:-150px;">
    
   <el-radio-button :label="false">展开</el-radio-button>
   <el-radio-button :label="true">收起</el-radio-button>
 </el-radio-group>
<el-menu default-active="1-4-1" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose" :collapse="isCollapse" style="margin-top:-140px;" >
  <el-menu-item index="2">
    <i class="el-icon-menu"></i>
    <span slot="title"><router-link to="/Default">系统首页</router-link></span>
  </el-menu-item>
  <el-submenu index="1">
    <template slot="title">
      <i class="el-icon-document"></i>
      <span slot="title">请假管理</span>
    </template>
    <el-menu-item-group>
        <template slot="title">
           <i class="el-icon-arrow-right"></i>
           <span slot="title"><router-link to="/AddLeave">假条申请</router-link></span>
           
        </template>
    </el-menu-item-group>
    <el-menu-item-group>
        <template slot="title">
           <i class="el-icon-arrow-right"></i>
           <span slot="title"><router-link to="/LeaveCancel">假条注销</router-link></span>
          
        </template>
    </el-menu-item-group>
   
  </el-submenu>
  <el-menu-item index="2">
    <i class="glyphicon glyphicon-user" style="padding-left:5px;"></i>
    <span slot="title" style="padding-left:12px;"><router-link to="/StudentInfo">学生信息</router-link> </span>
  </el-menu-item>
  <el-menu-item index="3" >
    <i class="el-icon-news"></i>
    <span slot="title"><router-link to="/ClassInfo">班级信息</router-link></span>
  </el-menu-item>
  <el-menu-item index="4">
    <i class="el-icon-sort"></i>
    <span slot="title"><router-link to="/GradeInfo">级别信息</router-link></span>
  </el-menu-item>
  
<el-submenu index="5">
    <template slot="title">
      <i class="el-icon-circle-plus-outline"></i>
      <span slot="title">添加信息</span>
    </template>
    <el-menu-item-group>
        <template slot="title">
           <i class="el-icon-arrow-right"></i>
           <span slot="title"><router-link to="/AddStudent">添加学生</router-link></span>
           <i class="el-icon-edit"></i>
        </template>
    </el-menu-item-group>
    <el-menu-item-group>
        <template slot="title">
           <i class="el-icon-arrow-right"></i>
           <span slot="title"><router-link to="/AddClass">添加班级</router-link></span>
           <i class="el-icon-edit"></i>
        </template>
    </el-menu-item-group>
   <el-menu-item-group>
        <template slot="title">
           <i class="el-icon-arrow-right"></i>
           <span slot="title"><router-link to="/AddGrade">添加级别</router-link></span>
           <i class="el-icon-edit"></i>
        </template>
    </el-menu-item-group>
  </el-submenu>
<el-submenu index="6">
    <template slot="title">
      <i class="el-icon-setting"></i>
      <span slot="title">用户管理</span>
    </template>
    <el-menu-item-group>
        <template slot="title">
           <i class="el-icon-arrow-right"></i>
           <span slot="title"><a href="#"><router-link to="/AddUser">添加用户</router-link></a></span>
               <i class="el-icon-service"></i>
        </template>
    </el-menu-item-group>
    <el-menu-item-group>
        <template slot="title">
           <i class="el-icon-arrow-right"></i>
           <span slot="title"><router-link to="/UpdateUser">修改密码</router-link></span>
               <i class="el-icon-document"></i>

        </template>
    </el-menu-item-group>
   
  </el-submenu>

</el-menu>
    </div>
</template>
<script>
export default {
    name:'left',
     data() {
      return {
        isCollapse: true
      };
    },
    methods: {
      handleOpen(key, keyPath) {
        console.log(key, keyPath);
      },
      handleClose(key, keyPath) {
        console.log(key, keyPath);
      }
    }
}
</script>
<style>
.el-menu-vertical-demo:not(.el-menu--collapse) {
    width: 200px;
    min-height: 400px;
  }
</style>


