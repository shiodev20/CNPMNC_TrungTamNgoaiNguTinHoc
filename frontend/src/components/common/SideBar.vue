<template>
  <div
    @click="condition_animate = !condition_animate"
    :class="condition_animate == false ? 'animation' : null"
    class="sidebar"
  >
    <div class="sidebar_logo">
      <a>Logo </a>
    </div>
    <div class="sidebar_list">
      <div class="sidebar_item">
        <font-awesome-icon
          class="icon"
          :class="condition_animate == false ? 'animation_parent_item' : null"
          :icon="['fas', 'house-user']"
        />
        <a :class="condition_animate == false ? 'animation_item' : null"
          >Trang chủ</a
        >
      </div>

      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'admin'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'file-signature']"
        />
        <a
          :class="condition_animate == false ? 'animation_item' : null"
          @click="this.$router.push({ name: 'exam' })"
          >Quản lý kỳ thi</a
        >
      </div>
      <div
        v-if="$store.state.acount.authencation && check_role() == false"
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'pen']"
        />
        <a
          @click="this.$router.push({ name: 'exambatch' })"
          :class="condition_animate == false ? 'animation_item' : null"
        >
          Quản lý đợt thi</a
        >
      </div>
      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'admin'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'clipboard-list']"
        /><a
          @click="this.$router.push({ name: 'course' })"
          :class="condition_animate == false ? 'animation_item' : null"
        >
          Quản lý khóa học</a
        >
      </div>
      <div
        v-if="$store.state.acount.authencation && check_role() == false"
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'chalkboard-user']"
        />
        <a
          @click="this.$router.push({ name: 'class' })"
          :class="condition_animate == false ? 'animation_item' : null"
        >
          Quản lý lớp học</a
        >
      </div>
      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'admin'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'location-dot']"
        />
        <a
          :class="condition_animate == false ? 'animation_item' : null"
          @click="this.$router.push({ name: 'branch' })"
        >
          Quản lý chi nhánh</a
        >
      </div>
      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'admin'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'earth-americas']"
        />
        <a
          @click="this.$router.push({ name: 'content_exam' })"
          :class="condition_animate == false ? 'animation_item' : null"
          >Quản lý hình thức thi</a
        >
      </div>
      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'admin'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'earth-americas']"
        />
        <a
          @click="this.$router.push({ name: 'languages' })"
          :class="condition_animate == false ? 'animation_item' : null"
          >Quản lý ngôn ngữ</a
        >
      </div>
      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'admin'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'graduation-cap']"
        />
        <a
          @click="this.$router.push({ name: 'cerstificate' })"
          :class="condition_animate == false ? 'animation_item' : null"
          >Quản lý chứng chỉ</a
        >
      </div>
      <div class="sidebar_item" v-if="check_role()">
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'clipboard-list']"
        /><a
          @click="this.$router.push({ name: 'list-course' })"
          :class="condition_animate == false ? 'animation_item' : null"
        >
          Xem khóa học</a
        >
      </div>
      <div class="sidebar_item" v-if="check_role()">
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'file-signature']"
        />
        <a
          :class="condition_animate == false ? 'animation_item' : null"
          @click="this.$router.push({ name: 'list-exam' })"
          >Xem kỳ thi</a
        >
      </div>
      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'user'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'clipboard-list']"
        /><a
          @click="this.$router.push({ name: 'list-myclass' })"
          :class="condition_animate == false ? 'animation_item' : null"
        >
          lớp học của tôi</a
        >
      </div>
      <div
        v-if="
          $store.state.acount.authencation &&
          $store.state.acount.authencation.role == 'user'
        "
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'file-signature']"
        />
        <a
          :class="condition_animate == false ? 'animation_item' : null"
          @click="this.$router.push({ name: 'list-myexam' })"
          >kỳ thi của tôi</a
        >
      </div>
      <div
        v-if="$store.state.acount.authencation && check_role() == false"
        class="sidebar_item"
      >
        <font-awesome-icon
          :class="condition_animate == false ? 'animation_parent_item' : null"
          class="icon"
          :icon="['fas', 'file-invoice-dollar']"
        />
        <a
          @click="this.$router.push({ name: 'bill' })"
          :class="condition_animate == false ? 'animation_item' : null"
        >
          Quản lý hóa đơn</a
        >
      </div>
    </div>
  </div>
</template>
<script>
import { ref } from "vue";
import store from "@/store";
export default {
  setup() {
    var condition_animate = ref(true);
    store.state.acount.authencation = store.getters["acount/check_token"]();
    const check_role = () => {
      if (store.state.acount.authencation) {
        if (
          store.state.acount.authencation.role == "admin" ||
          store.state.acount.authencation.role == "employee"
        ) {
          return false;
        }
      }
      return true;
    };
    return {
      condition_animate,
      check_role,
    };
  },
};
</script>
<style scoped>
/* animation sidebar */
.animation {
  width: var(--with_sidebar_hiden) !important;
}

/* animation item */
.animation_item {
  left: 10px !important;
  opacity: 0;
  pointer-events: none;
}
/* animation parnet item  */
.animation_parent_item {
  left: 50% !important;
  transform: translate(-50%, -50%) !important;
}

/* parent */
.sidebar {
  transition: 0.2s;
  background: var(--color_main);
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  width: var(--with_sidebar);
  box-shadow: 0px 4px 14px 14px rgba(0, 0, 0, 0.28);
}
a {
  text-decoration: none;
  text-align: center;
  font-family: Roboto !important;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}
/* logo */
.sidebar_logo {
  padding: 40px;
}
.sidebar_logo > a {
  color: white !important;
  font-size: 30px;
}

/* item */
.sidebar_item > a {
  font-size: 16px;
  color: #c1c662;
  left: 60px;
}
.sidebar_item > .icon {
  color: #c1c662;
  left: 30px;
  z-index: 10;
}
.sidebar_item > .icon,
.sidebar_item > a {
  top: 50%;
  position: absolute;
  transform: translateY(-50%);
  transition: 0.3s;
}
.sidebar_item {
  position: relative;
  padding: 25px;
  justify-content: start;
  text-align: center;
  align-items: center;
}
</style>
