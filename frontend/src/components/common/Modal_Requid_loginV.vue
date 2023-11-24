<template>
  <div v-if="status == true" class="modal">
    <div class="modal_deader">
      <h1>Thông báo</h1>
      <font-awesome-icon
        @click="(status = false), ($store.state.test.status_modal = false)"
        class="icon"
        :icon="['fas', 'xmark']"
      />
    </div>
    <p>
      Bạn chưa đăng nhập vào hệ thống. Vui lòng <strong>đăng nhập</strong> để sử
      dụng dịch vụ
    </p>
    <div class="btn">
      <button
        class="btn_out"
        @click="(status = false), ($store.state.test.status_modal = false)"
      >
        Thoát
      </button>
      <button
        class="btn_login"
        @click="
          this.$router.push({ name: 'acount', query: { action: 'login' } })
        "
      >
        Đến trang đăng nhập
      </button>
    </div>
  </div>
</template>
<script>
import { ref } from "vue";
export default {
  setup() {
    const status = ref(true);
    return { status };
  },
};
</script>
<style scoped>
.modal {
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  background: white;
  box-shadow: 0 0 1px black;
  border-radius: 10px;
  padding: 10px;
  text-align: start;
  transition: 0.3s;
  animation: animations 0.4s ease forwards;
}
strong {
  color: var(--color_table);
}
.btn {
  text-align: end;
  padding: 10px 0;
}
.btn > button {
  padding: 15px 20px;
  border-radius: 5px;
  border: none;
  cursor: pointer;
  margin-left: 10px;
}
.btn_login {
  background: var(--color_table);
  color: white;
}
.icon {
  font-size: 30px;
  margin-right: 20px;
}
.modal_deader {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
@keyframes animations {
  0% {
    top: -100%;
  }
  to {
    top: 10px;
  }
}
</style>
