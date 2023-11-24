<template>
  <div class="alert">
    <div class="parent">
      <span>
        <font-awesome-icon :icon="['fas', 'check']" />
      </span>
    </div>
    <span class="parent">
      <Strong>Success</Strong>
      <slot name="content">
        <p>Thêm chứng chỉ thành công</p>
      </slot>
    </span>
  </div>
</template>
<script>
import { watch } from "vue";
import store from "@/store";
export default {
  setup() {
    watch(store.state.certificate.alert, () => {
      console.log("Khôi");
    });
  },
};
</script>
<style scoped>
.alert {
  background: rgb(255, 255, 255);
  box-shadow: 0 0 2px rgba(0, 0, 0, 0.522);
  position: relative;
  overflow: hidden;
  display: flex;
  align-items: center;
  gap: 30px;
  border-radius: 10px;
  padding: 20px;
  animation: hides 3s ease forwards;
}
@keyframes hides {
  to {
    display: none !important;
  }
}
.parent {
  position: relative;
}
.parent:nth-child(1) > span {
  background: blue;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: white;
}
.alert::before {
  content: "";
  position: absolute;
  left: 0;
  width: 100%;
  bottom: 0;
  height: 3px;
  background: blue;
  animation: move 3s ease forwards;
}
@keyframes move {
  to {
    width: 0;
  }
}
</style>
