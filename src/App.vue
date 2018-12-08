<template>
  <div id="app">
   <router-view></router-view>
  </div>
</template>

<script>

// import Header from './components/Header'
 import Left from './components/Home/Left'
 import Login from './components/Login'
export default {
  name: 'App',
  components:{
    // headerInfo:Header,
     left:Left,
     login:Login
  },
  
}

</script>

<style>
 *{
   padding: 0px;
   margin: 0px;
 }
 body{
   background-color: #E9EEF3;
 }
 
</style>
