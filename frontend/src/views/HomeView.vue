<template>
  <basesidebar></basesidebar>
  <div class="main">
    <div class="container">
      <!-- acoutn name -->
      <div v-if="$store.state.acount.authencation" class="acount">
        <span class="data" @click="check"
          >Welcome back,
          {{
            $store.state.acount.authencation.lastname +
            " " +
            $store.state.acount.authencation.firtname
          }}<span class="user"
            ><font-awesome-icon :icon="['fas', 'user']" />
          </span>
        </span>
      </div>
      <div v-if="!this.$store.state.acount.authencation" class="acount">
        <span
          @click="
            this.$router.push({ name: 'acount', query: { action: 'signup' } })
          "
          >Đăng ký tài khoản</span
        >
        <span
          @click="
            this.$router.push({ name: 'acount', query: { action: 'login' } })
          "
          >Đăng nhập</span
        >
      </div>
      <!-- <baseadmin></baseadmin> -->
      <basecustomer></basecustomer>
    </div>
  </div>
</template>
<script>
import sidebar from "@/components/common/SideBar.vue";
// import admin from "@/views/page/Main/AminVV.vue";
import customer from "@/views/page/Main/CustomerVV.vue";
import store from "@/store";
import router from "@/router";
export default {
  components: {
    basesidebar: sidebar,
    // baseadmin: admin,
    basecustomer: customer,
  },
  name: "HomeView",
  setup() {
    const check = () => {
      if (store.state.acount.authencation) {
        router.push({ name: "acount", query: { action: "logout" } });
      } else {
        router.push({ name: "acount", query: { action: "login" } });
      }
    };
    store.dispatch("acount/get_all");
    store.getters["acount/check_token"]();
    return {
      check,
    };
  },
};
</script>
<style scoped>
.data {
  display: flex;
  gap: 10px;
  align-items: center;
}
.user {
  display: block;
  width: 30px;
  height: 30px;
  border: 2px solid var(--color_main);
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.main {
  padding-left: var(--with_sidebar);
  background: var(--color_background);
}
.container {
  min-height: 100vh;
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
}
.acount {
  position: absolute;
  top: 38px;
  right: 23px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  z-index: 300;
  display: flex;
  gap: 30px;
}
</style>
