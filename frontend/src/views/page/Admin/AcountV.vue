<template>
  <div class="acount">
    <baselogin v-if="this.$route.query.action == 'login'"></baselogin>
    <baselogout v-if="this.$route.query.action == 'logout'"></baselogout>
    <basesignup v-if="this.$route.query.action == 'signup'"></basesignup>
  </div>
</template>
<script>
import login from "@/components/acount/LoginV.vue";
import logout from "@/components/acount/logoutV.vue";
import signup from "@/components/acount/SignUP.vue";
export default {
  name: "ExamV",
  components: {
    baselogin: login,
    baselogout: logout,
    basesignup: signup,
  },
};
</script>
<style scoped>
.acount {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background: var(--color_main);
}
</style>
