<template>
  <div class="add_exam">
    <h1>
      {{ this.$route.query.id == "edit" ? "Thêm" : "Cập nhật" }} chứng chỉ
    </h1>
    <form @submit.prevent="submit" class="from_add_exam">
      <div class="group_add_exam">
        <div class="name_add_exam">
          <label>Tên Chứng chỉ</label>
          <input v-model="data.name" type="text" />
          <span v-if="err.name" class="err">{{ err.name }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <!-- dropdown lanquage -->
      </div>
      <!-- end group area -->
      <!-- button fished -->
      <div class="btn_fished">
        <button type="submit">
          {{ this.$route.query.action == "edit" ? "Cập nhật" : "Hoàn tất" }}
        </button>
      </div>
      <!-- end button fished -->
    </form>
  </div>
</template>
<script>
import { computed, ref } from "vue";
import store from "@/store";
export default {
  name: "HomeView",
  created() {
    const check = setInterval(() => {
      if (store.state.certificate.certificate != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          this.$route.query.action == "edit"
            ? ((store.state.certificate.data_post.id = Number(
                this.$route.query.id
              )),
              (store.state.certificate.data_post.name = store.getters[
                "certificate/getbyid"
              ](Number(this.$route.query.id))[0].name))
            : null;
        }
      }
    }, 200);
  },
  setup() {
    store.dispatch("certificate/get_all");
    const data = computed(() => {
      return store.state.certificate.data_post;
    });
    const err = computed(() => {
      return store.state.certificate.err;
    });
    const submit = () => {
      !data.value.name
        ? (err.value.name = "Không bỏ trống tên")
        : (err.value.name = null);
      !err.value.name
        ? data.value.id == null
          ? store.dispatch("certificate/create")
          : (store.dispatch("certificate/update"),
            (store.state.certificate.data_post.id = null))
        : null;
    };
    const alert = ref(false);
    return {
      alert,
      submit,
      data,
      err,
    };
  },
};
</script>
<style scoped>
/* // thong báo */
.group_alert {
  position: absolute;
  top: 100px;
  right: 10px;
  overflow: hidden;
}
/* // kết thúc thông báo */
.err {
  position: absolute;
  top: calc(100% + 10px);
  font-size: 14px;
  color: red;
}
/* button fished */
.btn_fished > button {
  border-radius: 10px;
  background: rgb(187, 4, 4);
  display: flex;
  width: 172px;
  height: 44px;
  padding: 5px 11px;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 10px;
  color: white;
  border: none;
}
.btn_fished {
  padding: 30px;
  display: flex;
  justify-content: center;
  align-items: center;
}
/* and button fished */
/* common  */
input {
  color: var(--color_main) !important;
  text-align: center;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  width: 294px;
  height: var(--size-field_height);
  flex-shrink: 0;
  border-radius: 10px;
  border: 1px solid var(--color_main);
}
/* text area */
.group_area_exam {
  align-items: start;
  display: flex;
  flex-direction: column;
}
textarea {
  resize: none;
  outline: none;
  width: 671px;
  border: 1px solid var(--color_main);
  min-height: 148px;
  border-radius: 10px;
}
/* text area end */
.group_check_exam,
.group_area_exam {
  text-align: start;
}
.group_check {
  padding: 20px 0;
}
/* check radio */
.radio {
  display: none;
}
.icon_check {
  position: absolute;
  top: 50%;
  left: 50%;
  display: none;
  transform: translate(-50%, -50%);
}
.radio:checked ~ .btn > .icon_check {
  display: block;
}
.btn {
  position: relative;
  width: 25px;
  display: block;
  height: 25px;
  border-radius: 5px;
  border: 1px solid var(--color_main);
}
.group_check {
  display: flex;
  gap: 30px;
}
.group_check > label {
  display: flex;
  gap: 14px;
  align-items: center;
}
/* end radio */
/* set input  */
label {
  margin-bottom: 9px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
/* end set input */
.name_add_exam {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  position: relative;
}
.from_add_exam {
  border-radius: 10px;
  background: white;
  padding-top: 50px;
  padding-left: 50px;
  padding-right: 50px;
}

/* group name and language */
.group_add_exam > div {
  width: 330px;
}
.group_add_exam > div > input {
  width: 100% !important;
}
.group_add_exam {
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin-bottom: 20px;
}
/* end group name and language */
h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  width: 299px;
  position: relative;
}
.dropdown_input {
  position: relative;
  height: var(--size-field_height);
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
  font-size: 16px;
}
.dropdown_menu {
  transition: 0.1s;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  pointer-events: none;
  z-index: 1;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
/* end drop down  */
</style>
