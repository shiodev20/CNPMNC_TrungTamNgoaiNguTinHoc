<template>
  <form @submit.prevent="submit" v-if="data != undefined" class="login">
    <h1>LOGO</h1>

    <div class="name">
      <label>Tên đăng nhập</label>
      <input v-model="data.emailUser" type="text" />
      <span v-if="err.emailUser" class="err">{{ err.emailUser }}</span>
    </div>

    <div class="password">
      <label>Mật khẩu</label>
      <input v-model="data.passwordUser" type="password" />
      <span v-if="err.passwordUser" class="err">{{ err.passwordUser }}</span>
    </div>
    <div class="btn_submit">
      <button type="submit">Đăng nhập</button>
      <span>hoặc</span>
      <span
        class="register"
        @click="
          this.$router.push({ name: 'acount', query: { action: 'signup' } })
        "
        >Đăng ký</span
      >
    </div>
  </form>
</template>
<script>
import { computed } from "vue";
import store from "@/store";
import router from "@/router";
export default {
  name: "ExamV",
  setup() {
    store.dispatch("acount/get_all");
    const data = computed(() => {
      return store.state.acount.data_post;
    });
    const err = computed(() => {
      return store.state.acount.err;
    });
    const submit = () => {
      !data.value.emailUser
        ? (err.value.emailUser = "Không bỏ trống email")
        : (err.value.emailUser = null);
      !data.value.passwordUser
        ? (err.value.passwordUser = "Không bỏ trống mật khẩu")
        : (err.value.passwordUser = null);
      !err.value.passwordUser && !err.value.emailUser ? fake_login() : null;
    };

    const fake_login = () => {
      for (var item in store.state.acount.emloyee) {
        if (store.state.acount.emloyee[item].email == data.value.emailUser) {
          if (
            store.state.acount.emloyee[item].password == data.value.passwordUser
          ) {
            store.state.acount.authencation = store.state.acount.emloyee[item];
            localStorage.clear();
            localStorage.setItem(
              "token",
              store.state.acount.authencation.token
            );
            if (store.state.acount.authencation.role == "user") {
              router.push({ name: "english" });
            } else {
              router.push({ name: "admin" });
            }
          } else {
            err.value.passwordUser = "mật khẩu không chính xác";
          }
        }
      }
    };
    return {
      submit,
      data,
      err,
    };
  },
};
</script>
<style scoped>
.err {
  position: absolute;
  top: calc(100% + 10px);
  font-size: 14px;
  color: red;
}
.register {
  font-weight: 600;
}
i {
  margin-left: 200px;
}
.forget {
  display: flex;
  justify-content: end !important;
}
.login {
  width: 566px;
  height: 568px;
  border-radius: 10px;
  background: white;
  box-shadow: 0px 4px 4px 9px rgba(0, 0, 0, 0.25);
  flex-shrink: 0;
  padding: 20px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 40px;
}
h1 {
  margin: 0;
}
.name > label,
.password > label {
  color: var(--color_main);
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  margin-bottom: 5px;
  margin-left: 10px;
}
.name,
.password {
  display: flex;
  flex-direction: column;
  text-align: start;
  position: relative;
}
input {
  width: 325px;
  height: 49px;
  padding-left: 10px;
  flex-shrink: 0;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}
.btn_submit {
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  gap: 10px;
}
button {
  border: none;
  background: var(--color_main);
  color: white;
  padding: 20px 50px;
  border-radius: 10px;
}
</style>
