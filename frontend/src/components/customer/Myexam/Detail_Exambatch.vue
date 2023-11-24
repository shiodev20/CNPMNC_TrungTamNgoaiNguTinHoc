<template>
  <div v-if="data.id && exam && exambatch" class="list_exam">
    <h1>Lịch thi</h1>
    <div class="Upperbar">
      <div class="upperbar_add">
        <div class="upperbar_item">
          <label>Tên đợt thi </label>
          <input :value="data.name" disabled type="text" />
        </div>
        <div class="upperbar_item">
          <label>Thuộc kỳ thi</label>
          <input :value="exam_id(data.exam_id).name" disabled type="text" />
        </div>
      </div>
    </div>

    <div v-if="this.$route.query.add == undefined" class="list_exam_child">
      <!-- List exam -->
      <div class="list_exams">
        <table v-if="list_customer.length > 0" class="table">
          <tr>
            <th>Id</th>
            <th>Hình thức thi</th>
            <th>Giờ bắt đầu</th>
            <th>Giờ kết thúc</th>
            <th>Ngày thi</th>
            <th>Địa điểm thi</th>
          </tr>
          <tr
            :id="'item' + item.id"
            v-for="item in list_customer"
            :key="item.id"
          >
            <td>{{ item.id }}</td>
            <td>{{ content_id(item.exam_type)[0].name }}</td>
            <td>{{ item.start_hour }}</td>
            <td>{{ item.end_hour }}</td>
            <td>{{ item.start_date }}</td>
            <td>{{ branch_id(item.branch_id)[0].adress }}</td>
          </tr>
        </table>
        <div class="images" v-else>
          <img src="../../Imagess/notfound.png" alt="" />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed } from "vue";
import store from "@/store";
export default {
  name: "HomeView",
  created() {
    const check = setInterval(() => {
      if (store.state.exambatch.exambatch != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          this.$route.query.action == "detail"
            ? ((store.state.exambatch.data_post.id = Number(
                this.$route.query.id
              )),
              (store.state.exambatch.data_post.name = store.getters[
                "exambatch/getbyid"
              ](Number(this.$route.query.id))[0].name),
              (store.state.exambatch.data_post.exam_id = store.getters[
                "exambatch/getbyid"
              ](Number(this.$route.query.id))[0].exam_id),
              (store.state.exambatch.data_post.exam_data = store.getters[
                "exambatch/getbyid"
              ](Number(this.$route.query.id))[0].exam_data),
              (store.state.exambatch.data_post.status = store.getters[
                "exambatch/getbyid"
              ](Number(this.$route.query.id))[0].status),
              (store.state.test.chose_type =
                store.state.exambatch.data_post.exam_data[0].exam_type))
            : null;
        }
      }
    }, 200);
  },
  setup() {
    store.dispatch("exambatch/get_all");
    store.dispatch("exam/get_all");
    store.dispatch("cus_exam/get_all");
    store.dispatch("content_exame/get_all");
    store.dispatch("branch/get_all");
    store.dispatch("acount/get_all");
    store.dispatch("clipboard/get_all");
    const customer_id = (value) => {
      return store.getters["acount/getbyid"](value);
    };
    const branch_id = (value) => {
      return store.getters["branch/getbyid"](value);
    };
    //get type name
    const content = computed(() => {
      return store.state.content_exame.content_exame;
    });
    const content_id = (value) => {
      return store.getters["content_exame/getbyid"](value);
    };
    const exam = computed(() => {
      return store.state.exam.exam;
    });
    const exambatch = computed(() => {
      return store.state.exambatch.exambatch;
    });
    const data = computed(() => {
      return store.state.exambatch.data_post;
    });
    const exam_id = (value) => {
      return store.getters["exam/getbyid"](value);
    };
    // dat list pay true and dependent exambatch
    //data
    // get type depen exambatch
    const type_exambatch = computed(() => {
      return store.state.exambatch.data_post.exam_data;
    });

    // get data depen type

    const list_customer = computed(() => {
      return store.state.exambatch.data_post.exam_data;
    });

    return {
      list_customer,
      onsubmit,
      data,
      exambatch,
      exam_id,
      exam,
      type_exambatch,
      content,
      content_id,
      customer_id,
      branch_id,
    };
  },
};
</script>
<style scoped>
.list_exam {
  min-width: 1000px;
}
.btn_posin {
  padding: 10px;
  width: 50px;
}
.atten_parent {
  position: absolute;
  bottom: 20px;
  left: 50%;
  transform: translateX(-50%);
}
.atten_parent > button {
  padding: 10px 30px;
  background: var(--color_main);
  border-radius: 5px;
  color: white;
}
.chonse {
  display: flex;
  align-items: center;
  gap: 20px;
}
.parent_customer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.btn_delete {
  position: absolute;
  bottom: 15px;
  left: 50%;
  transform: translateX(-50%);
}
.btn_delete > button {
  background: red;
  padding: 7px 30px;
  border-radius: 5px;
  border: none;
  color: white;
}
.add_cus {
  display: flex;
  justify-content: center;
  align-items: center;
  font-weight: 700;
}
.lb_filter_cus > input {
  display: none;
}

.lb_filter_cus > span {
  padding: 10px;
  border: 2px solid var(--color_main);
  border-radius: 10px;
  position: relative;
}
.lb_filter_cus > span > strong {
  position: absolute;
  right: -10px;
  top: -10px;
  display: block;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  color: white;
  background: red;
}
.lb_filter_cus > input:checked ~ span {
  color: rgb(193, 182, 182);
  background: var(--color_main);
}
.filter_customer {
  display: flex;
  justify-content: start;
  padding-bottom: 10px;
  gap: 10px;
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
  position: relative;
}

.group_check {
  justify-content: center;
  display: flex;
}
.group_check > label {
  display: flex;
  gap: 14px;
  align-items: center;
}
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
.radio {
  display: none;
}
.radio:checked ~ .btn > .icon_check {
  display: block;
}
.icon_check {
  position: absolute;
  top: 50%;
  left: 50%;
  display: none;
  transform: translate(-50%, -50%);
}

.btnadd {
  border: none;
  border-radius: 10px;
  background: rgb(192, 2, 2);
  color: white;
  padding: 10px 40px;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  bottom: 10px;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
/* list exam  */
.select_table > label {
  width: 124px;
  height: 33px;
  flex-shrink: 0;
  border-radius: 10px 10px 0px 0px;
  border-bottom: none !important;
  border: 1px solid var(--color_main);
  color: rgb(67, 62, 62);
  position: relative;
}
.select_table > label > input {
  display: none;
}
.select_table > label > input:checked ~ .bg {
  background: #088395;
  color: white;
}
.bg {
  position: absolute;
  inset: 0;
  display: flex;
  justify-content: center;
  border-radius: 10px 10px 0px 0px;
  align-items: center;
  border-bottom: none !important;
  cursor: pointer;
  background: #0e6d7b;
}
.select_table {
  position: absolute;
  display: flex;
  top: -33px;
}
table {
  border-collapse: collapse;
  position: relative;
  width: 100%;
}
.tale {
  margin-left: auto;
  margin-right: auto;
}

th,
td {
  padding: 8px;
  text-align: center;
  vertical-align: middle !important;
}

tr:nth-child(even) {
  background-color: var(--color_row);
}

th {
  background-color: var(--color_table);
  color: white;
}
.setting {
  display: flex;
  justify-content: center;
}
.setting > button {
  border-radius: 20px;
  background: white;
  border: none;
  margin-left: 10px;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.icon_trash,
.icon_edit,
.icon,
.icon_detail {
  font-size: 20px;
  padding: 5px;
}
.icon_trash {
  color: red;
}
.icon {
  color: green;
}
.icon_edit {
  color: rgb(85, 250, 85);
}

/* end list exam  */
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
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 15px !important;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  transform: translate(-50%, -50%);
  white-space: nowrap;
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  width: 180px;
  margin: 10px;
  position: relative;
}
.dropdown_input {
  position: relative;
  padding: 20px 20px;
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
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

h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* upperbar */
.Upperbar {
  display: flex;
  height: 72px;
  padding: 5px 15px;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
  background: white;
  border-radius: 10px;
  position: relative;
}
.upperbar_button > button {
  width: 146px;
  height: 33px;
  border-radius: 10px;
  background: var(--color_main);
  color: white;
  font-size: 13px;
}
.upperbar_button {
  position: absolute;
  right: 10px;
  bottom: 20px;
}
.upperbar_add {
  display: flex;
  justify-content: start;
  gap: 10px;
}
.upperbar_add > div {
  display: flex;
  flex-direction: column;
  align-items: start;
}
.upperbar_add > div > input {
  width: 146px;
  height: 33px;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}
.upperbar_add > div > label {
  margin-bottom: 5px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}
/* end upperbar */

/* list parent  */
.list_exam_child {
  background: white;
  border-radius: 10px;
  padding: 0 20px;
  padding-bottom: 67px;
  position: relative;
  padding-top: 50px;
  margin-top: 10px;
  min-height: 400px;
}
/* end list parent  */
</style>
