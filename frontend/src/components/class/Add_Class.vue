<template>
  <div class="add_exam">
    <h1>
      {{ this.$route.query.action == "edit" ? "Cập nhật" : "Thêm" }} lớp học
    </h1>
    <form @submit.prevent="onsubmit" class="from_add_exam">
      <div class="group_add_exam">
        <div class="name_add_exam">
          <label>Tên lớp học</label>
          <input v-model="data.name" type="text" />
          <span class="err" v-if="err.name">{{ err.name }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <div class="language_add_exam">
          <label> Tên khóa học </label>
          <div class="dropdown">
            <div
              @click="status_dropdown_course = !status_dropdown_course"
              class="dropdown_input"
            >
              <span class="text">
                {{
                  data.course_id == null
                    ? "Chọn khóa học"
                    : $store.getters["course/getbyid"](data.course_id)[0].name
                }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="
                status_dropdown_course == true ? 'dropdown_menu_show' : null
              "
              class="dropdown_menu"
            >
              <div
                @click="
                  (data.course_id = item.id), (status_dropdown_course = false)
                "
                class="dropdown_item"
                v-for="item in course"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
          <span class="err" v-if="err.course">{{ err.course }}</span>
        </div>
        <!-- dropdown lanquage -->
      </div>

      <!-- //group price and ducation -->
      <div class="group_add_exam">
        <div class="price_add_exam">
          <label>Ngày bắt đầu</label>
          <input v-model="data.start_date" type="date" />
          <span class="err" v-if="err.start_date">{{ err.start_date }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <div class="price_add_exam">
          <label>Ngày kết thúc</label>
          <input v-model="data.end_date" type="date" />
          <span class="err" v-if="err.end_date">{{ err.end_date }}</span>
        </div>
        <!-- dropdown lanquage -->
      </div>
      <div class="group_add_exam">
        <div class="language_add_exam">
          <label> Chi nhánh </label>
          <div class="dropdown">
            <div
              @click="status_dropdown_branch = !status_dropdown_branch"
              class="dropdown_input"
            >
              <span class="text">
                {{
                  data.branch_id == null
                    ? "Chọn chi nhánh"
                    : $store.getters["branch/getbyid"](data.branch_id)[0].name
                }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="
                status_dropdown_branch == true ? 'dropdown_menu_show' : null
              "
              class="dropdown_menu"
            >
              <div
                @click="
                  (data.branch_id = item.id), (status_dropdown_branch = false)
                "
                class="dropdown_item"
                v-for="item in branch"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
          <span class="err" v-if="err.branch">{{ err.branch }}</span>
        </div>
      </div>
      <!-- end grop price and ducation -->

      <!-- group area -->
      <!-- end group area -->
      <!-- button fished -->
      <div class="btn_fished">
        <button>
          {{ this.$route.query.action == "edit" ? "Cập nhật" : "Hoàn tất" }}
        </button>
      </div>
      <!-- end button fished -->
    </form>
  </div>
</template>
<script>
import { computed, reactive, ref } from "vue";
import store from "@/store";
import { useToast } from "vue-toastification";
export default {
  name: "HomeView",
  created() {
    const check = setInterval(() => {
      if (store.state.class.class != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          this.$route.query.action == "edit"
            ? ((store.state.class.data_post.id = Number(this.$route.query.id)),
              (store.state.class.data_post.name = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].name),
              (store.state.class.data_post.course_id = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].course_id),
              (store.state.class.data_post.branch_id = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].branch_id),
              (store.state.class.data_post.start_date = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].start_date),
              (store.state.class.data_post.end_date = store.getters[
                "class/getbyid"
              ](Number(this.$route.query.id))[0].end_date))
            : null;
        }
      }
    }, 200);
  },
  setup() {
    const toast = useToast();
    store.dispatch("class/get_all");
    store.dispatch("course/get_all");
    store.dispatch("branch/get_all");
    const status_dropdown_course = ref(null);
    const status_dropdown_branch = ref(null);
    const branch = computed(() => {
      return store.state.branch.branch;
    });
    const course = computed(() => {
      return store.state.course.course;
    });
    const data_fetch = reactive({
      name: null,
      id: null,
    });
    const status_dropdown = ref(false);
    const err = computed(() => {
      return store.state.class.err;
    });
    const data = computed(() => {
      return store.state.class.data_post;
    });
    const convert_time = (value) => {
      return new Date(value).getTime();
    };
    const reset_data = () => {
      data.value.name = null;
      data.value.course_id = null;
      data.value.start_date = null;
      data.value.end_date = null;
      data.value.branch_id = null;
    };
    const onsubmit = () => {
      data.value.name == null || data.value.name == ""
        ? (err.value.name = "Không bỏ trống tên lớp học")
        : (err.value.name = null);

      data.value.course_id == null
        ? (err.value.course = "Không bỏ trống khóa học")
        : (err.value.course = null);

      data.value.start_date == null
        ? (err.value.start_date = "Không bỏ trống ngày bắt đầu")
        : convert_time(data.value.start_date) <
          convert_time(new Date().getTime())
        ? (err.value.start_date = "Ngày bắt đầu không hợp lệ")
        : (err.value.start_date = null);

      data.value.end_date == null
        ? (err.value.end_date = "Không bỏ trống ngày kết thúc")
        : convert_time(data.value.start_date) >=
          convert_time(data.value.end_date)
        ? (err.value.end_date = "Ngày kết thúc không hợp lệ")
        : (err.value.end_date = null);

      data.value.branch_id == null
        ? (err.value.branch = "Không bỏ trống chi nhánh")
        : (err.value.branch = null);

      err.value.name == null &&
      err.value.start_date == null &&
      err.value.end_date == null &&
      err.value.branch == null
        ? data.value.id == null
          ? (store.dispatch("class/create"),
            toast.success("Tạo thành công"),
            reset_data())
          : (store.dispatch("class/update"),
            toast.success("Cập nhật thành công"))
        : data.value.id == null
        ? toast.error("Tạo thất bại")
        : toast.error("Cập nhật thất bại");
    };
    return {
      err,
      data_fetch,
      data,
      status_dropdown,
      onsubmit,
      status_dropdown_course,
      status_dropdown_branch,
      branch,
      course,
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
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  height: var(--size-field_height);
  border-radius: 10px;
  border: 1px solid var(--color_main);
}
/* text area */
.group_area_exam {
  align-items: start;
  display: flex;
  flex-direction: column;
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
.name_add_exam,
.language_add_exam,
.price_add_exam,
.ducation_add_exam {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
.from_add_exam {
  border-radius: 10px;
  background: white;
  padding-top: 50px;
  padding-left: 50px;
  padding-right: 50px;
}

/* group name and language */
.group_add_exam {
  display: grid;
  grid-template-columns: 330px 330px;
  gap: 20px;
  margin-bottom: 40px;
}
.group_add_exam > div {
  position: relative;
  display: flex !important;
}
.group_add_exam > div > input,
.group_add_exam > div > div {
  width: 100% !important;
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
  position: relative;
}

.dropdown_input {
  position: relative;
  height: var(--size-field_height);
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-size: 14px;
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
