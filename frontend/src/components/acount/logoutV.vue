<template>
  <div class="logout">
    <span>BẠN MUỐN ĐĂNG XUẤT ?</span>
    <div>
      <button @click="logout">LOGOUT</button>
    </div>
  </div>
</template>
<script>
import store from "@/store";
import router from "@/router";
export default {
  setup() {
    const logout = () => {
      localStorage.clear();
      store.state.acount.authentication = null;
      router.push({ name: "home" });
    };
    return { logout };
  },
};
</script>
<style>
.logout {
  display: flex;
  gap: 50px;
  padding: 70px;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  transform: rotate(0.138deg);
  flex-shrink: 0;
  border-radius: 10px;
  background: white;
  box-shadow: 0px 4px 4px 9px rgba(0, 0, 0, 0.25);
}
.logout > span {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 30px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}
.logout > div > button {
  border: none;
  display: flex;
  width: 172px;
  height: 44px;
  padding: 5px 11px;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 10px;
  border-radius: 10px;
  background: rgba(187, 0, 0, 1);
  color: white;
}
</style>
